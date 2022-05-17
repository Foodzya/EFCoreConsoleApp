using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        public Task<ProductCategory> GetByIdAsync(int productCategoryId);
        public Task<List<ProductCategory>> GetAllForSectionAsync(int sectionId);
        public Task CreateAsync(ProductCategory productCategory);
        public void Remove(ProductCategory productCategory);
        public void Update(ProductCategory productCategory);
        public Task<ProductCategory> GetByNameAsync(string productCategoryName);
        public Task SaveChangesAsync();
    }
}
