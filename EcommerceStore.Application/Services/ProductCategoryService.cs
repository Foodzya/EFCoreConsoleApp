using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        public Task CreateProductCategoryAsync(ProductCategoryInputModel productCategoryInputModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductCategoryViewModel>> GetAllProductCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategoryViewModel> GetProductCategoryByIdAsync(int productCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductCategoryAsync(int productCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryInputModel productCategoryInputModel)
        {
            throw new NotImplementedException();
        }
    }
}
