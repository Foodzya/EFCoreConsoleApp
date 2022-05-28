using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IProductCategoryService
    {
        public Task<ProductCategoryViewModel> GetProductCategoryByIdAsync(int productCategoryId);
        public Task<List<ProductCategoryViewModel>> GetAllProductCategoriesForSectionAsync(int sectionId);
        public Task LinkSubcategoryToParentCategoryAsync(int parentCategoryId, int subcategoryId);
        public Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryInputModel productCategoryInputModel);
        public Task RemoveProductCategoryAsync(int productCategoryId);
        public Task CreateProductCategoryAsync(ProductCategoryInputModel productCategoryInputModel);
    }
}
