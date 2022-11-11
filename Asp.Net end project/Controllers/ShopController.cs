using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helper;
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
        public async Task<ActionResult> Index(/*int page =1,int take = 6*/ )
        {
            IEnumerable<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Include(m => m.Categories)
                .Include(m => m.ProductImages)
                //.Skip((page * take)-take)           
                //.Take(take)
                .ToListAsync();
            IEnumerable<Categories> categories = await _context.Categories
                .Where(m => !m.IsDeleted)
                .Include(m=>m.Products)
                .Skip(6)
                .ToListAsync();

            ShopVM shopVM = new ShopVM
            {
                Products = products,
                Categories = categories
            };

            //int count = await GetPageCount(take);

            //Paginate<ShopVM> result = new Paginate<ShopVM>(shopVM, page, count);
            return View(shopVM);
        }

        //private async Task<int> GetPageCount(int take)
        //{
        //    int productCount = await _context.Products.Where(m => !m.IsDeleted).CountAsync();

        //    return (int)Math.Ceiling((decimal)productCount / take);
        //}

        //private List<ProductListVM> GetMapDatas(List<Product> products)
        //{
        //    List<ProductListVM> productList = new List<ProductListVM>();

        //    foreach (var product in products)
        //    {
        //        ProductListVM newProduct = new ProductListVM
        //        {
        //            Id = product.Id,
        //            Title = product.Title,
        //            Description = product.Description,
        //            MainImage = product.ProductImages.Where(m => m.IsMain).FirstOrDefault()?.Image,
        //            CategoryName = product.Category.Name,
        //            Price = product.Price
        //        };

        //        productList.Add(newProduct);
        //    }

        //    return productList;
        //}
    }
}
