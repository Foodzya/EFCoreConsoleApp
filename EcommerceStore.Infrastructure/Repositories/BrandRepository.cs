using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Infrastucture.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly EcommerceContext _context;

        public BrandRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            var brands = await _context.Brands
                .Include(b => b.Products)
                .ToListAsync();

            return brands;
        }

        public async Task<Brand> GetByIdAsync(int brandId)
        {
            var brand = await _context.Brands
            .Include(b => b.Products)
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync();

            return brand;
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public async Task RemoveAsync(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Brand> GetByNameAsync(string brandName)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brandName);

            return brand;
        }
    }
}