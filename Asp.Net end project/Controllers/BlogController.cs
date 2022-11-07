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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.IsDeleted).ToListAsync();
            IEnumerable<Customer> customer = await _context.Customers
                .Where(m => !m.IsDeleted)
                .Include(m => m.Socials)
                .ToListAsync();

            BlogVM blogVM = new BlogVM
            {
                Blogs = blogs,
                Customers = customer,

            };


            return View(blogVM);
        }
    }
}
