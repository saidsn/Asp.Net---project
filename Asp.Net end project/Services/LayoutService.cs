using Asp.Net_end_project.Data;
using Asp.Net_end_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string,string>> GetDatasFromSetting()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);

        }
        public async Task<IEnumerable<Currency>> GetDatasFromCurrency()
        {
            return await _context.Currencies.Where(m=>!m.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<Language>> GetDatasFromLanguage()
        {
            return await _context.Languages.Where(m => !m.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<Categories>> GetDatasFromCategories()
        {
            return await _context.Categories.Where(m => !m.IsDeleted).Take(6).ToListAsync();
        }

    }
}
