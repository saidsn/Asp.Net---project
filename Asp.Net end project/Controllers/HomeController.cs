using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.Services;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        public HomeController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }
 
      
        public async Task<IActionResult> Index()
        {
            //HttpContext.Session.SetString("name", "Emil"); //sessiona data elave olunur
            //Response.Cookies.Append("surname", "Abdullayev",new CookieOptions {MaxAge=TimeSpan.FromDays(5) }); // Cookie-ye data elave olunur

            Dictionary<string, string> datas = await _layoutService.GetDatasFromSetting();

            int productTake = int.Parse(datas["HomeProductTake"]);

            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.IsDeleted).ToListAsync();

            IEnumerable<Service> services = await _context.Services.Where(m => !m.IsDeleted).ToListAsync();

            OurProduct ourProduct = await _context.OurProducts.Where(m => !m.IsDeleted).FirstOrDefaultAsync();

            IEnumerable<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Where(m=>m.SellerCount > 0)
                .Include(m => m.ProductImages)
                .Take(productTake).ToListAsync();

            IEnumerable<Banner> banners = await _context.Banners.Where(m => !m.IsDeleted).ToListAsync();

            TopSeller topSeller = await _context.TopSellers.Where(m => !m.IsDeleted).FirstOrDefaultAsync();

            SingleBanner singleBanner = await _context.SingleBanners.Where(m => !m.IsDeleted).FirstOrDefaultAsync();

            OurBlog ourBlog = await _context.OurBlogs.Where(m => !m.IsDeleted).FirstOrDefaultAsync();

            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.IsDeleted).Take(4).ToListAsync();

            IEnumerable<Brand> brands = await _context.Brands.Where(m => !m.IsDeleted).ToListAsync();



            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                Services = services,
                OurProduct = ourProduct,
                Products = products,
                Banners = banners,
                TopSeller = topSeller,
                SingleBanner = singleBanner,
                OurBlog = ourBlog,
                Blogs = blogs,
                Brands = brands
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            var dbProduct = await GetProductById(id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> basket = GetBasket();

            UpdateBasket(basket, dbProduct.Id);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction(nameof(Index));

            

        }



        private void UpdateBasket(List<BasketVM> basket, int id)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == id);

            if (existProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

        }


        private async Task<Product> GetProductById(int? id)
        {
            return await _context.Products.FindAsync(id);
        }


        private List<BasketVM> GetBasket()
        {

            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            return basket;
        }

    }
}
