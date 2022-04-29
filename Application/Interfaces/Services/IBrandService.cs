using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Infrastucture.Persistence.Models.InputModels;
using EcommerceStore.Infrastucture.Persistence.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        public Task<BrandViewModel> GetByIdAsync(int brandId);
        public Task<IEnumerable<BrandViewModel>> GetAllAsync();
        public Task AddAsync(BrandInputModel brandIm);
        public Task RemoveByIdAsync(int brandId);
        public Task ModifyAsync(int brandId, BrandInputModel brandIm);
    }
}