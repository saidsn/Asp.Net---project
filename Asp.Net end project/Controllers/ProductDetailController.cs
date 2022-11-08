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
    public class ProductDetailController : Controller
    {
        private readonly AppDbContext _context;
        public ProductDetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            Product products = await _context.Products
                   .Where(m => !m.IsDeleted && m.Id == id)
                   .Include(m => m.ProductImages)
                   .FirstOrDefaultAsync();

            ProductDetailVM productDetailVM = new ProductDetailVM
            {
                Products = products,

            };


            return View(productDetailVM);
        }
    }
}
