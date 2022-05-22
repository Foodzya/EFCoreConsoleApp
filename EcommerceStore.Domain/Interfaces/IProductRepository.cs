using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(int productId);
        public Task<List<Product>> GetByIdsAsync(List<int> productIds);
        public Task<List<Product>> GetAllAsync();
        public Task CreateAsync(Product product);
        public void Remove(Product product);
        public void Update(Product product);
        public Task<Product> GetByNameAsync(string productName);
        public Task SaveChangesAsync();
    }
}