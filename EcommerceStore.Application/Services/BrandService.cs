using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task CreateBrandAsync(BrandInputModel brandIm)
        {
            var existingBrand = await _brandRepository.GetByNameAsync(brandIm.Name);

            if (existingBrand.Name != null)
                throw new ValidationException(AlreadyExistExceptionMessages.BrandAlreadyExists, existingBrand.Id);

            var brand = new Brand
            {
                Name = brandIm.Name,
                FoundationYear = brandIm.FoundationYear
            };

            await _brandRepository.CreateAsync(brand);

            await _brandRepository.SaveChangesAsync();
        }

        public async Task<List<BrandViewModel>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();

            if (brands is null)
                throw new ValidationException(NotFoundExceptionMessages.BrandNotFound);

            var brandsViewModel = brands
                .Select(b => new BrandViewModel
                {
                    Name = b.Name,
                    FoundationYear = b.FoundationYear,
                    ProductsCount = b.Products.Count()
                })
                .ToList();

            return brandsViewModel;
        }

        public async Task<BrandViewModel> GetBrandByIdAsync(int brandId)
        {
            var brand = await _brandRepository.GetByIdAsync(brandId);

            if (brand is null)
                throw new ValidationException(NotFoundExceptionMessages.BrandNotFound, brandId);

            var brandViewModel = new BrandViewModel
            {
                Name = brand.Name,
                FoundationYear = brand.FoundationYear,
                ProductsCount = brand.Products.Count()
            };

            return brandViewModel;
        }

        public async Task UpdateBrandAsync(int brandId, BrandInputModel brandIm)
        {
            var existingBrand = await _brandRepository.GetByNameAsync(brandIm.Name);

            if (existingBrand != null && existingBrand.Id != brandId)
                throw new ValidationException(AlreadyExistExceptionMessages.BrandAlreadyExists, brandId);

            var brand = await _brandRepository.GetByIdAsync(brandId);

            brand.Name = brandIm.Name;
            brand.FoundationYear = brandIm.FoundationYear;

            _brandRepository.Update(brand);

            await _brandRepository.SaveChangesAsync();
        }

        public async Task RemoveBrandByIdAsync(int brandId)
        {
            var brand = await _brandRepository.GetByIdAsync(brandId);

            if (brand is null)
                throw new ValidationException(NotFoundExceptionMessages.BrandNotFound, brandId);

            brand.IsDeleted = true;

            _brandRepository.Update(brand);

            await _brandRepository.SaveChangesAsync();
        }
    }
}