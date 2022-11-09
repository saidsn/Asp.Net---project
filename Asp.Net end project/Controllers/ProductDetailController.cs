using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.Services;
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
        private readonly LayoutService _layoutService;
        public ProductDetailController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            Product products = await _context.Products
                   .Where(m => !m.IsDeleted && m.Id == id)
                   .Include(m => m.ProductImages)
                   .FirstOrDefaultAsync();
            Dictionary<string, string> setting = await _layoutService.GetDatasFromSetting();


            ProductDetailVM productDetailVM = new ProductDetailVM
            {
                Products = products,
                Settings = setting,


            };


            return View(productDetailVM);
        }
    }
}
