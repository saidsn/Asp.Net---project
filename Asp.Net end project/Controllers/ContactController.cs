using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            Contact contact = await _context.Contacts.Where(m => !m.IsDeleted).FirstOrDefaultAsync();
            SendMessage sendMessage = await _context.SendMessages.Where(m => !m.IsDeleted).FirstOrDefaultAsync();

            ContactVM contactVM = new ContactVM
            {
                Contact = contact,
                SendMessage = new SendMessage()

            };

            return View(contactVM);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SendMessage sendMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }

                bool isExsist = await _context.SendMessages.AnyAsync(m => m.Name.Trim() == sendMessage.Name.Trim() &&
                     m.Email.Trim() == sendMessage.Email.Trim() &&
                     m.Message.Trim() == sendMessage.Message.Trim() &&
                     m.Phone.Trim() == sendMessage.Phone.Trim() &&
                     m.Subject.Trim() == sendMessage.Subject.Trim());

                if (isExsist)
                {
                    ModelState.AddModelError("Name", "Subject is already exist");
                }
                await _context.SendMessages.AddAsync(sendMessage);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
