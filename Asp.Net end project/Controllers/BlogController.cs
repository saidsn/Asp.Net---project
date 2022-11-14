using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            List<Blog> blogs = await _context.Blogs
                .Where(m => !m.IsDeleted)
                .Skip((page * take) - take)
                .Take(take)
                .OrderBy(m => m.Id)
                .ToListAsync();

            IEnumerable<Blog> recentPosts = await _context.Blogs.Where(m => !m.IsDeleted).OrderByDescending(m => m.Id).ToListAsync();

            IEnumerable<Customer> customer = await _context.Customers
                .Where(m => !m.IsDeleted)
                .Include(m => m.Socials)
                .ToListAsync();

            IEnumerable<Tag> tags = await _context.Tags.Where(m => !m.IsDeleted).ToListAsync();

            int count = await GetPageCount(take);

            List<BlogVM> shopList = new List<BlogVM>();

            BlogVM blogVM = new BlogVM
            {
                Blogs = blogs,
                Customers = customer,
                ResentPosts = recentPosts,
                Tags = tags,
            };

            shopList.Add(blogVM);

            Paginate<BlogVM> result = new Paginate<BlogVM>(shopList, page, count);

            return View(result);
        }

        private async Task<int> GetPageCount(int take)
        {
            int productCount = await _context.Products.Where(m => !m.IsDeleted).CountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }
    }
}
