using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IProductService 
    {
        public Task<ProductViewModel> GetProductByIdAsync(int productId);
        public Task<List<ProductViewModel>> GetAllProductsAsync();
        public Task CreateProductAsync(ProductInputModel productInputModel);
        public Task UpdateProductAsync(int productId, ProductInputModel productInputModel);
        public Task RemoveProductByIdAsync(int productId);
        public Task<bool> IsProductAvailableInStockAsync(int productId, decimal productQuantity);
    }
}