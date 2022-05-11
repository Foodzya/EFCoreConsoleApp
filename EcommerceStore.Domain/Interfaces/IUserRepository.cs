using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int userId);
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByEmailAsync(string email);
        public Task CreateAsync(User user);
        public void Remove(User user);
        public void Update(User user);
        public Task SaveChangesAsync();
    }
}