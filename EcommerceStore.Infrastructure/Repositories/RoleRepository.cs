using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EcommerceContext _context;

        public RoleRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<Role> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public void Remove(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }
    }
}
