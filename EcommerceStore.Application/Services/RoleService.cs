using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            if (roles is null)
                throw new ValidationException(ExceptionMessages.RoleNotFound);

            var rolesViewModel = roles
                .Where(r => !r.IsDeleted)
                .Select(r => new RoleViewModel
                {
                    Name = r.Name
                })
                .ToList();

            return rolesViewModel;
        }

        public async Task<RoleViewModel> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role is null || role.IsDeleted)
                throw new ValidationException(ExceptionMessages.RoleNotFound, roleId);

            var roleViewModel = new RoleViewModel
            {
                Name = role.Name
            };

            return roleViewModel;
        }

        public async Task RemoveRoleByIdAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role is null || role.IsDeleted)
                throw new ValidationException(ExceptionMessages.RoleNotFound, roleId);

            role.IsDeleted = true;

            _roleRepository.Update(role);

            await _roleRepository.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(int roleId, RoleInputModel roleInputModel)
        {
            var existingRole = await _roleRepository.GetByNameAsync(roleInputModel.Name);

            if (existingRole != null && existingRole.Id != roleId)
                throw new ValidationException(ExceptionMessages.RoleAlreadyExists, roleId);

            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role.IsDeleted)
                throw new ValidationException(ExceptionMessages.RoleNotFound, roleId);

            role.Name = roleInputModel.Name;

            _roleRepository.Update(role);

            await _roleRepository.SaveChangesAsync();
        }

        public async Task CreateRoleAsync(RoleInputModel roleInputModel)
        {
            var existingRole = await _roleRepository.GetByNameAsync(roleInputModel.Name);

            if (existingRole != null)
                throw new ValidationException(ExceptionMessages.RoleAlreadyExists, existingRole.Id);

            var role = new Role
            {
                Name = roleInputModel.Name
            };

            await _roleRepository.CreateAsync(role);

            await _roleRepository.SaveChangesAsync();
        }
    }
}