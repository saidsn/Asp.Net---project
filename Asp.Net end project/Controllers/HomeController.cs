
using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.Services;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

 
    }
}
