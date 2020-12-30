using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext context;

        public LanguageRepository(BookStoreContext _context)
        {
            context = _context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            var data = await context.Languages.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Text = x.Text,
                Description = x.Description
            }).ToListAsync();
            return data;
        }
    }
}
