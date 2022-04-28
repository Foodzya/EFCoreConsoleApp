using EcommerceStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.Application.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        public Task<Brand> GetByIdAsync(int id);
        public Task<IEnumerable<Brand>> GetAllAsync();
        public Task RemoveByIdAsync(int id);
        public Task ModifyAsync(Brand brand);
    }
}
