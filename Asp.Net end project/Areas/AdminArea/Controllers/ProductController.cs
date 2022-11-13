using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Include(m => m.ProductImages)
                .Include(m => m.Categories)
                .ToListAsync();

            return View(products);
        }

    

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products
            .Where(m => !m.IsDeleted)
            .Include(m => m.ProductImages)
            .Include(m => m.Categories)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Products
                .Where(m => !m.IsDeleted && m.Id == id)
                .Include(m => m.ProductImages)
                .FirstOrDefaultAsync();

            if (product == null) return NotFound();

            foreach (var item in product.ProductImages)
            {
                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/product", item.Image);
                Helper.DeleteFile(path);
                item.IsDeleted = true;
            }

            product.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Categories> categories = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Where(m => !m.IsDeleted)
                .Include(m => m.Categories)
                .Include(m => m.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private decimal StringToDecimal(string str)
        {
            return decimal.Parse(str.Replace(".", ","));
        }
    }
}
