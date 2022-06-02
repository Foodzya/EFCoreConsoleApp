using EcommerceStore.API.Authentication;
using EcommerceStore.API.Constants;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Role controller for managing roles using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get list of roles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of roles is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpGet]
        [ProducesResponseType(typeof(List<RoleViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RoleViewModel>>> GetAllAsync()
        {
            var rolesViewModel = await _roleService.GetAllRolesAsync();

            return Ok(rolesViewModel);
        }

        /// <summary>
        /// Get an existing role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when role is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpGet("{roleId}")]
        [ProducesResponseType(typeof(RoleViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<RoleViewModel>> GetByIdAsync([FromRoute] int roleId)
        {
            var roleViewModel = await _roleService.GetRoleByIdAsync(roleId);

            return Ok(roleViewModel);
        }

        /// <summary>
        /// Deletes an existing role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when role is successfully deleted</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpDelete("{roleId}")]
        [ProducesResponseType(typeof(RoleViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveByIdAsync([FromRoute] int roleId)
        {
            await _roleService.RemoveRoleByIdAsync(roleId);

            return Ok();
        }

        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="roleInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "name": "Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Returns when role is successfully created</response>
        /// <response code="400">Returns when role input details are incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] RoleInputModel roleInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _roleService.CreateRoleAsync(roleInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "name": "Super Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Returns when role is successfully updated</response>
        /// <response code="400">Returns when role input details are incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPut("{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int roleId, [FromBody] RoleInputModel roleInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _roleService.UpdateRoleAsync(roleId, roleInputModel);

            return Ok();
        }
    }
}