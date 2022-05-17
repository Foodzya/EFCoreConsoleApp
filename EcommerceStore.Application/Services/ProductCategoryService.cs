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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ISectionService _sectionService;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ISectionService sectionService)
        {
            _productCategoryRepository = productCategoryRepository;
            _sectionService = sectionService;
        }

        public async Task CreateProductCategoryAsync(ProductCategoryInputModel productCategoryInputModel)
        {
            var existingProductCategory = await _productCategoryRepository.GetByNameAsync(productCategoryInputModel.Name);

            if (existingProductCategory != null)
                throw new ValidationException(AlreadyExistExceptionMessages.ProductCategoryAlreadyExists);

            var productCategory = new ProductCategory
            {
                Name = productCategoryInputModel.Name,
                ParentCategoryId = productCategoryInputModel.ParentCategoryId
            };

            await _sectionService.LinkCategoryToSectionAsync(productCategoryInputModel);

            await _productCategoryRepository.CreateAsync(productCategory);

            await _productCategoryRepository.SaveChangesAsync();
        }

        public async Task<List<ProductCategoryViewModel>> GetAllProductCategoriesForSectionAsync(int sectionId)
        {
            var productCategories = await _productCategoryRepository.GetAllForSectionAsync(sectionId);

            if (productCategories is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductCategoryNotFound);

            var productCategoriesViewModel = productCategories
                .Select(p => new ProductCategoryViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Products = p.Products?
                        .Select(p => new ProductViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Quantity = p.Quantity,
                            Price = p.Price,
                            Description = p.Description,
                            Image = p.Image,
                            BrandName = p.Brand.Name,
                        })
                        .ToList(),
                })
                .ToList();

            return productCategoriesViewModel;
        }

        public async Task<ProductCategoryViewModel> GetProductCategoryByIdAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            if (productCategory is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductCategoryNotFound, productCategoryId);

            var productCategoryViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Products = productCategory.Products
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Quantity = p.Quantity,
                        Price = p.Price,
                        Description = p.Description,
                        Image = p.Image,
                        BrandName = p.Brand.Name
                    })
                    .ToList()
            };

            return productCategoryViewModel;
        }

        public async Task LinkSubcategoryToParentCategoryAsync(int parentCategoryId, int subcategoryId)
        {
            var parentCategory = await _productCategoryRepository.GetByIdAsync(parentCategoryId);

            var subcategory = await _productCategoryRepository.GetByIdAsync(subcategoryId);

            if (parentCategory is null || subcategory is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductCategoryNotFound);

            subcategory.ParentCategoryId = parentCategory.Id;

            _productCategoryRepository.Update(subcategory);

            await _productCategoryRepository.SaveChangesAsync();
        }

        public async Task RemoveProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            if (productCategory is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductCategoryNotFound, productCategoryId);

            productCategory.IsDeleted = true;

            _productCategoryRepository.Update(productCategory);

            await _productCategoryRepository.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryInputModel productCategoryInputModel)
        {
            var existingProductCategory = await _productCategoryRepository.GetByNameAsync(productCategoryInputModel.Name);

            if (existingProductCategory != null && existingProductCategory.Id != productCategoryId)
                throw new ValidationException(AlreadyExistExceptionMessages.ProductCategoryAlreadyExists, productCategoryId);

            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            productCategory.Name = productCategoryInputModel.Name;
            productCategory.ParentCategoryId = productCategoryInputModel.ParentCategoryId;

            _productCategoryRepository.Update(productCategory);

            await _productCategoryRepository.SaveChangesAsync();
        }
    }
}