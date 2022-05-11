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

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task CreateProductCategoryAsync(ProductCategoryInputModel productCategoryInputModel)
        {
            var existingProductCategory = await _productCategoryRepository.GetByNameAsync(productCategoryInputModel.Name);

            if (existingProductCategory != null)
                throw new ValidationException(ExceptionMessages.ProductCategoryAlreadyExists);

            var productCategory = new ProductCategory
            {
                Name = productCategoryInputModel.Name,
                ParentCategoryId = productCategoryInputModel.ParentCategoryId
            };

            await _productCategoryRepository.CreateAsync(productCategory);
        }

        public async Task<List<ProductCategoryViewModel>> GetAllProductCategoriesAsync()
        {
            var productCategories = await _productCategoryRepository.GetAllAsync();

            if (productCategories is null)
                throw new ValidationException(ExceptionMessages.ProductCategoryNotFound);

            var productCategoriesViewModel = productCategories
                .Select(p => new ProductCategoryViewModel
                {
                    Name = p.Name,
                    Products = p.Products?
                        .Select(p => new ProductViewModel
                        {
                            Name = p.Name,
                            Quantity = p.Quantity,
                            Price = p.Price,
                            Description = p.Description,
                            Image = p.Image,
                            BrandName = p.Brand.Name,
                            ProductCategoryName = p.ProductCategory.Name
                        })
                        .ToList()
                })
                .ToList();

            return productCategoriesViewModel;
        }

        public async Task<ProductCategoryViewModel> GetProductCategoryByIdAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            if (productCategory is null)
                throw new ValidationException(ExceptionMessages.ProductCategoryNotFound, productCategoryId);

            var productCategoryViewModel = new ProductCategoryViewModel
            {
                Name = productCategory.Name,
                Products = productCategory.Products
                    .Select(p => new ProductViewModel
                    {
                        Name = p.Name,
                        Quantity = p.Quantity,
                        Price = p.Price,
                        Description = p.Description,
                        Image = p.Image,
                        BrandName = p.Brand.Name,
                        ProductCategoryName = p.ProductCategory.Name
                    })
                    .ToList()
            };

            return productCategoryViewModel;
        }

        public async Task RemoveProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            if (productCategory is null)
                throw new ValidationException(ExceptionMessages.ProductCategoryNotFound, productCategoryId);

            _productCategoryRepository.Remove(productCategory);

            await _productCategoryRepository.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryInputModel productCategoryInputModel)
        {
            var existingProductCategory = await _productCategoryRepository.GetByNameAsync(productCategoryInputModel.Name);

            if (existingProductCategory != null && existingProductCategory.Id != productCategoryId)
                throw new ValidationException(ExceptionMessages.ProductCategoryAlreadyExists, productCategoryId);

            var productCategory = await _productCategoryRepository.GetByIdAsync(productCategoryId);

            productCategory.Name = productCategoryInputModel.Name;
            productCategory.ParentCategoryId = productCategoryInputModel.ParentCategoryId;

            _productCategoryRepository.Update(productCategory);

            await _productCategoryRepository.SaveChangesAsync();
        }
    }
}