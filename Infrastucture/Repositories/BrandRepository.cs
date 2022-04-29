using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Interfaces.Repositories;
using EcommerceStore.Core.Entities;
using EcommerceStore.Infrastucture.Persistence.Context;
using EcommerceStore.Infrastucture.Persistence.Models.InputModels;
using EcommerceStore.Infrastucture.Persistence.Models.ViewModels;
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

        public async Task AddAsync(BrandInputModel brandIm)
        {
            var brand = new Brand
            {
                Name = brandIm.Name,
                FoundationYear = brandIm.FoundationYear
            };

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllAsync()
        {
            var brandsViewModel = await _context.Brands
            .Select(b => new BrandViewModel
            {
                Name = b.Name,
                FoundationYear = b.FoundationYear,
                ProductsCount = b.Products.Count()
            })
            .ToListAsync();

            return brandsViewModel;
        }

        public async Task<BrandViewModel> GetByIdAsync(int brandId)
        {
            var brandViewModel = await _context.Brands
            .Where(b => b.Id == brandId)
            .Select(b => new BrandViewModel
            {
                Name = b.Name,
                FoundationYear = b.FoundationYear,
                ProductsCount = b.Products.Count()
            })
            .FirstOrDefaultAsync();

            if (brandViewModel == null)
                throw new NullReferenceException("No brand found");

            return brandViewModel;
        }

        public async Task ModifyAsync(int brandId, BrandInputModel brandIm)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brandId);

            brand.Name = brandIm.Name;
            brand.FoundationYear = brandIm.FoundationYear;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(int brandId)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null || brand.IsDeleted)
            {
                throw new NullReferenceException("Brand was null");
            }

            brand.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}