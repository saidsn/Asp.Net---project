using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
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


            return View();
        }
    }
}
