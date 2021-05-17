using DigitalLibrary.Data;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{

    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookContext _context = null;
        public LanguageRepository(BookContext context)
        {
            _context = context;
        }
        public async Task<List<LanguageModel>> GetAllLanguage()
        {
            return await _context.Languages.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
    }
}
