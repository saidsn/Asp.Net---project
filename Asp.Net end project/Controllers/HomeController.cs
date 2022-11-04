
using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
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
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
 

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.IsDeleted).ToListAsync();
            IEnumerable<Currency> currencies = await _context.Currencies.Where(m => !m.IsDeleted).ToListAsync();
            IEnumerable<Language> languages = await _context.Languages.Where(m => !m.IsDeleted).ToListAsync();

            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                Currencies = currencies,
                Languages = languages
            };

            return View(model);
        }

 
    }
}
