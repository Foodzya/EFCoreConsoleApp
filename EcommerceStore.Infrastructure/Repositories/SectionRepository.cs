using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly EcommerceContext _context;

        public SectionRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
        }

        public async Task<List<Section>> GetAllAsync()
        {
            var sections = await _context.Sections
                .Include(s => s.ProductCategorySections)
                    .ThenInclude(p => p.ProductCategory)
                .Where(s => !s.IsDeleted)
                .ToListAsync();

            return sections;
        }

        public async Task<Section> GetByIdAsync(int sectionId)
        {
            var section = await _context.Sections
                .Include(s => s.ProductCategorySections)
                    .ThenInclude(p => p.ProductCategory)
                .Where(s => !s.IsDeleted)
                .FirstOrDefaultAsync(s => s.Id == sectionId);

            return section;
        }

        public async Task<Section> GetByNameAsync(string sectionName)
        {       
            var section = await _context.Sections
                .Include(s => s.ProductCategorySections)
                    .ThenInclude(p => p.ProductCategory)
                .Where(s => !s.IsDeleted)
                .FirstOrDefaultAsync(s => s.Name == sectionName);

            return section;
        }

        public void Remove(Section section)
        {
            _context.Sections.Remove(section);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Section section)
        {
            _context.Sections.Update(section);
        }
    }
}