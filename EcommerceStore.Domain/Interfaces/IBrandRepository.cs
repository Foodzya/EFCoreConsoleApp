using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IBrandRepository
    {
        public Task<Brand> GetByIdAsync(int brandId);
        public Task<List<Brand>> GetAllAsync();
        public Task CreateAsync(Brand brand);
        public void Remove(Brand brand);
        public void Update(Brand brand);
        public Task SaveChangesAsync();
        public Task<Brand> GetByNameAsync(string brandName);
    }
}