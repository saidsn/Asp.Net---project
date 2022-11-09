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
                SendMessage = sendMessage,

            };

            return View(contactVM);
        }
    }
}
