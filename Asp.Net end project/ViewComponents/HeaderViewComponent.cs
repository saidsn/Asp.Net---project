using Asp.Net_end_project.Data;
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
    public class HeaderViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        public HeaderViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _layoutService.GetDatasFromSetting();
            IEnumerable<Currency> currencies = await _layoutService.GetDatasFromCurrency();
            IEnumerable<Language> languages = await _layoutService.GetDatasFromLanguage();

            HeaderVM model = new HeaderVM
            {
                Settings = settings,
                Currencies = currencies,
                Languages = languages
            };
            return await Task.FromResult(View(model));
            
        }


         
    }
}
