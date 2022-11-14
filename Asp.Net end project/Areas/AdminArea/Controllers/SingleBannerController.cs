using Asp.Net_end_project.Data;
using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SingleBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SingleBannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            SingleBanner singleBanner = await _context.SingleBanners.Where(m => !m.IsDeleted).AsNoTracking().FirstOrDefaultAsync();
            ViewBag.count = await _context.SingleBanners.Where(m => !m.IsDeleted).CountAsync();
            return View(singleBanner);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SingleBanner banner)
        {
            if (!ModelState.IsValid) return View();

            if (!banner.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose correct image type");
                return View();
            }

            if (!banner.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Please choose correct image size");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + banner.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/banner", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await banner.Photo.CopyToAsync(stream);
            }

            banner.Image = fileName;

            await _context.SingleBanners.AddAsync(banner);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            SingleBanner banner = await _context.SingleBanners.FindAsync(id);

            if (banner == null) return NotFound();

            return View(banner);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();

                SingleBanner banner = await _context.SingleBanners.FirstOrDefaultAsync(m => m.Id == id);

                if (banner is null) return NotFound();

                return View(banner);

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SingleBanner banner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(banner);
                }

                if (!banner.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Please choose correct image type");
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + "_" + banner.Photo.FileName;

                SingleBanner dbBanner = await _context.SingleBanners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                if (dbBanner is null) return NotFound();

                if (dbBanner.Photo == banner.Photo)
                {
                    return RedirectToAction(nameof(Index));
                }

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/banner", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await banner.Photo.CopyToAsync(stream);
                }

                banner.Image = fileName;

                _context.SingleBanners.Update(banner);

                await _context.SaveChangesAsync();

                string dbPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider", dbBanner.Image);

                Helper.DeleteFile(dbPath);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {

            SingleBanner banner = await GetByIdAsync(id);

            if (banner == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider", banner.Image);

            Helper.DeleteFile(path);

            _context.SingleBanners.Remove(banner);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<SingleBanner> GetByIdAsync(int id)
        {
            return await _context.SingleBanners.FindAsync(id);
        }
    }
}
