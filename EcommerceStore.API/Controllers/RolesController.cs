using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleViewModel>>> GetAllAsync()
        {
            var rolesViewModel = await _roleService.GetAllRolesAsync();

            return Ok(rolesViewModel);
        }

        [HttpGet("{roleId}")]

        public async Task<ActionResult<RoleViewModel>> GetByIdAsync([FromRoute] int roleId)
        {
            var roleViewModel = await _roleService.GetRoleByIdAsync(roleId);

            return Ok(roleViewModel);
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult> RemoveByIdAsync([FromRoute] int roleId)
        {
            await _roleService.RemoveRoleByIdAsync(roleId);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] RoleInputModel roleInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _roleService.CreateRoleAsync(roleInputModel);

            return Ok();
        }

        [HttpPut("{roleId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int roleId, [FromBody] RoleInputModel roleInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _roleService.UpdateRoleAsync(roleId, roleInputModel);

            return Ok();
        }
    }
}