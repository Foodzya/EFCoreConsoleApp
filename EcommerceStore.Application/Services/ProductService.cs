using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductInputModel productInputModel)
        {
            var product = new Product
            {
                Name = productInputModel.Name,
                Price = productInputModel.Price,
                Quantity = productInputModel.Quantity,
                Description = productInputModel.Description,
                Image = productInputModel.Image
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
                    Image = p.Image
                })
                .ToList();

            return productsViewModel;
        }

        public Task<ProductViewModel> GetProductByIdAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateProductAsync(int productId, ProductInputModel productInputModel)
        {
            throw new System.NotImplementedException();
        }
    }
}