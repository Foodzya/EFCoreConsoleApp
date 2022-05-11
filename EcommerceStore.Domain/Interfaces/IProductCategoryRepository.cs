using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        public Task<ProductCategory> GetByIdAsync(int productCategoryId);
        public Task<List<ProductCategory>> GetAllAsync();
        public Task CreateAsync(ProductCategory productCategory);
        public void Remove(ProductCategory productCategory);
        public void Update(ProductCategory productCategory);
        public Task SaveChangesAsync();
    }
}
