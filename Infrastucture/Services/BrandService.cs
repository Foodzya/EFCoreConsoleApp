using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Interfaces.Repositories;
using EcommerceStore.Application.Interfaces.Services;
using EcommerceStore.Infrastucture.Persistence.Models.InputModels;
using EcommerceStore.Infrastucture.Persistence.Models.ViewModels;

namespace EcommerceStore.Infrastucture.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task AddAsync(BrandInputModel brandIm)
        {
            await _brandRepository.AddAsync(brandIm);
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<BrandViewModel> GetByIdAsync(int brandId)
        {
            return await _brandRepository.GetByIdAsync(brandId);
        }

        public async Task ModifyAsync(int brandId, BrandInputModel brandIm)
        {
            await _brandRepository.ModifyAsync(brandId, brandIm);
        }

        public async Task RemoveByIdAsync(int brandId)
        {
            await _brandRepository.RemoveByIdAsync(brandId);
        }
    }
}