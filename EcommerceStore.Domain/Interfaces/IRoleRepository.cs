using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IRoleRepository
    {
        public Task<Role> GetByIdAsync(int roleId);
        public Task<List<Role>> GetAllAsync();
        public Task CreateAsync(Role role);
        public void Remove(Role role);
        public void Update(Role role);
        public Task<Role> GetByNameAsync(string roleName);
        public Task SaveChangesAsync();
    }
}