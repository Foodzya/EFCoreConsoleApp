using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Application.Exceptions;

namespace EcommerceStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductInputModel productIm)
        {
            var existingProduct = await _productRepository.GetByNameAsync(productIm.Name);

            if (existingProduct != null)
                throw new ValidationException(AlreadyExistExceptionMessages.ProductAlreadyExists, existingProduct.Id);

            var product = new Product
            {
                Name = productIm.Name,
                Price = productIm.Price,
                Quantity = productIm.Quantity,
                Description = productIm.Description,
                Image = productIm.Image,
                BrandId = productIm.BrandId,
                ProductCategoryId = productIm.ProductCategoryId
            };

            await _productRepository.CreateAsync(product);

            await _productRepository.SaveChangesAsync();
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productsViewModel = products
                .Select(p => new ProductViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Description = p.Description,
                    Image = p.Image,
                    BrandName = p.Brand.Name,
                })
                .ToList();

            return productsViewModel;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductNotFound, productId);

            var productViewModel = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Quantity = product.Quantity,
                BrandName = product.Brand.Name,
            };

            return productViewModel;
        }

        public async Task<bool> IsProductAvailableInStockAsync(int productId, decimal productQuantity)
        {
            var product = await _productRepository.GetByIdAsync(productId); 

            if (product is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductNotFound, productId);

            if (product.Quantity >= productQuantity)
                return true;
            else
                return false; 
        }

        public async Task RemoveProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                throw new ValidationException(NotFoundExceptionMessages.ProductNotFound, productId);

            _productRepository.Remove(product);

            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int productId, ProductInputModel productIm)
        {
            var existingProduct = await _productRepository.GetByNameAsync(productIm.Name);

            if (existingProduct != null && existingProduct.Id != productId)
                throw new ValidationException(AlreadyExistExceptionMessages.ProductAlreadyExists);

            var product = await _productRepository.GetByIdAsync(productId);

            product.Name = productIm.Name;
            product.Description = productIm.Description;
            product.Price = productIm.Price;
            product.Quantity = productIm.Quantity;
            product.Image = productIm.Image;
            product.BrandId = productIm.BrandId;
            product.ProductCategoryId = productIm.ProductCategoryId;

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync();
        }
    }
}