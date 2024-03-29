﻿using Asp.Net_end_project.Data;
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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> Blogs = await _context.Blogs.Where(m => !m.IsDeleted).ToListAsync();
            return View(Blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();

            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose correct image type");
                return View();
            }

            if (!blog.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Please choose correct image size");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await blog.Photo.CopyToAsync(stream);
            }

            blog.Image = fileName;

            await _context.Blogs.AddAsync(blog);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Blog blog = await _context.Blogs.FindAsync(id);

            if (blog == null) return NotFound();

            return View(blog);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();

                Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

                if (blog is null) return NotFound();

                return View(blog);

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(blog);
                }
                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Please choose correct image type");
                    return View();
                }

                if (!blog.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Please choose correct image size");
                    return View();
                }
                string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;
                Blog dbBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (dbBlog is null) return NotFound();

                if (dbBlog.Photo == blog.Photo)
                {
                    return RedirectToAction(nameof(Index));
                }

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await blog.Photo.CopyToAsync(stream);
                }

                blog.Image = fileName;


                _context.Blogs.Update(blog);

                await _context.SaveChangesAsync();

                string dbPath = Helper.GetFilePath(_env.WebRootPath, "img", blog.Image);

                Helper.DeleteFile(dbPath);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        private async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }
    }
}
