using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels;
using Asp.Net_end_project.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Hosting;
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
        public async Task<ActionResult> Index(int page = 1, int take = 4)
        {
            List<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Include(m => m.Categories)
                .Include(m => m.ProductImages)
                .Skip((page * take) - take)
                .Take(take)
                .OrderBy(m => m.Id)
                .ToListAsync();

            IEnumerable<Categories> categories = await _context.Categories
                .Where(m => !m.IsDeleted)
                .Skip(6)
                .ToListAsync();

            int count = await GetPageCount(take);

            List<ShopVM> shopList = new List<ShopVM>();

            ShopVM model = new ShopVM
            {
                Products = products,
                Categories = categories
            };

            shopList.Add(model);

            Paginate<ShopVM> result = new Paginate<ShopVM>(shopList, page, count);

            return View(result);
        }

        private async Task<int> GetPageCount(int take)
        {
            int productCount = await _context.Products.Where(m => !m.IsDeleted).CountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }
    }
}  


