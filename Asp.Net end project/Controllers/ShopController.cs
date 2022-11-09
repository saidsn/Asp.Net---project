using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Include(m => m.ProductImages)
                .ToListAsync();


            ShopVM shopVM = new ShopVM
            {
                Products = products,

            };
            return View(shopVM);

            
        }
    }
}
