using Asp.Net_end_project.Data;
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
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _context;
        public BlogDetailController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            Blog blog = await _context.Blogs.Where(m => !m.IsDeleted).FirstOrDefaultAsync(m=>m.Id == id);
            IEnumerable<Blog> recentPosts = await _context.Blogs.Where(m => !m.IsDeleted).OrderByDescending(m => m.Id).ToListAsync();
            IEnumerable<Customer> customer = await _context.Customers
                .Where(m => !m.IsDeleted)
                .Include(m => m.Socials)
                .ToListAsync();
            IEnumerable<Tag> tags = await _context.Tags.Where(m => !m.IsDeleted).ToListAsync();

            BlogDetailVM blogDetailVM = new BlogDetailVM
            {
                Blog = blog,
                Customers = customer,
                ResentPosts = recentPosts,
                Tags = tags
            };


            return View(blogDetailVM);
        }
    }
}
