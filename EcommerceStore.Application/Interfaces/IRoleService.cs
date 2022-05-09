using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IRoleService
    {
        public Task<RoleViewModel> GetRoleByIdAsync(int roleId);
        public Task<List<RoleViewModel>> GetAllRolesAsync();
        public Task CreateRoleAsync(RoleInputModel roleInputModel);
        public Task RemoveRoleByIdAsync(int roleId);
        public Task UpdateRoleAsync(int roleId, RoleInputModel roleInputModel);
        public Task SaveChangesAsync();
    }
}
