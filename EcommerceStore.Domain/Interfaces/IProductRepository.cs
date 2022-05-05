using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(int productId);
        public Task<List<Product>> GetAllAsync();
        public Task CreateAsync(Product product);
        public Task RemoveAsync(Product product);
        public Task UpdateAsync(Product product);
        public Task SaveChangesAsync();
    }
}