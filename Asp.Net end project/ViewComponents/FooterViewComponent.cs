using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.Services;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        private readonly AppDbContext _context;

        public FooterViewComponent(LayoutService layoutService, AppDbContext context)
        {
            _layoutService = layoutService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Categories> categories = await _layoutService.GetDatasFromCategories();
            Dictionary<string, string> settings = await _layoutService.GetDatasFromSetting();
            List<BasketDetailVM> basketDetailsList = new List<BasketDetailVM>();

            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketItems = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

                List<BasketDetailVM> basketDetail = new List<BasketDetailVM>();

                foreach (var item in basketItems)
                {
                    Product product = await _context.Products
                        .Where(m => m.Id == item.Id && m.IsDeleted == false)
                        .Include(m => m.ProductImages).FirstOrDefaultAsync();


                    BasketDetailVM newBasket = new BasketDetailVM
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Image = product.ProductImages.Where(m => m.IsMain).FirstOrDefault().Image,
                        Price = product.Price,
                        Count = item.Count,
                        DiscountPrice = product.DiscountPrice,
                        Total = (product.Price - ((product.Price / 100) * product.DiscountPrice)) * item.Count
                    };

                    basketDetail.Add(newBasket);
                }
                FooterVM footerVM = new FooterVM
                {
                    BasketDetails = basketDetail,
                    Categories = categories,
                    Settings = settings

                };
                return await Task.FromResult(View(footerVM));
            }
            else
            {
                FooterVM footerVM = new FooterVM
                {
                    BasketDetails = basketDetailsList,
                    Categories = categories,
                    Settings = settings

                };
                return await Task.FromResult(View(footerVM));
            }
        }
    }
}
