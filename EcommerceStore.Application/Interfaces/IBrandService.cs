using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IBrandService
    {
        public Task<BrandViewModel> GetBrandByIdAsync(int brandId);
        public Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync();
        public Task CreateBrandAsync(BrandInputModel brandInputModel);
        public Task RemoveBrandByIdAsync(int brandId);
        public Task UpdateBrandAsync(int brandId, BrandInputModel brandInputModel);
    }
}