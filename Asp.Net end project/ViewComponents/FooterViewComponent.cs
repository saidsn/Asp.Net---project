using Asp.Net_end_project.Models;
using Asp.Net_end_project.Services;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;

        public FooterViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Categories> categories = await _layoutService.GetDatasFromCategories();
            Dictionary<string, string> settings = await _layoutService.GetDatasFromSetting();

            FooterVM model = new FooterVM
            {
                Settings = settings,
                Categories = categories
            };
            return await Task.FromResult(View(model));
        }
    }
}
