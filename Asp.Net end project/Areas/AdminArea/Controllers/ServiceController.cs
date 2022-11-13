using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Service> services = await _context.Services.Where(m => !m.IsDeleted).ToListAsync();
            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Service service = await _context.Services.FindAsync(id);

            if (service == null) return NotFound();

            return View(service);
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            Service service = await GetByIdAsync(id);



            if (service == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/icon", service.Image);

            Helper.DeleteFile(path);


            _context.Services.Remove(service);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }
    }
}
