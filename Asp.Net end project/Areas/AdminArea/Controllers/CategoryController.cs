using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Categories> categories = await _context.Categories
                .Where(m => !m.IsDeleted)
                .OrderByDescending(m => m.Id)
                .Skip(6)
                .Take(6)
                .ToListAsync();
            ViewBag.count = await _context.Categories.Where(m => !m.IsDeleted).CountAsync();
            return View(categories);
        }
    }
}
