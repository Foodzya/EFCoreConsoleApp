using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.API.Constants;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// User controller for managing users using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<ActionResult<UserViewModel>> AuthenticateAsync([FromBody] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ExceptionMessages.IncorrectUserCredentials);

            var userViewModel = await _userService.AuthenticateUserAsync(userLoginModel);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when user is successfully obtained</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserViewModel>> GetByIdAsync([FromRoute] int userId)
        {
            var userViewModel = await _userService.GetUserByIdAsync(userId);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Get a list of products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of users is successfully obtained</response>
        [Authorize(Roles = Roles.admin)]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserViewModel>>> GetAllAsync()
        {
            var usersViewModel = await _userService.GetAllUsersAsync();

            return Ok(usersViewModel);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userInputModel"></param>
        /// <returns></returns>
        /// <response code="200">Returns when user is successfully created</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UserInputModel userInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _userService.CreateUserAsync(userInputModel);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when user is successfully deleted</response>
        /// <response code="400">Returns when failed during user deleting</response>
        [Authorize(Roles = Roles.admin)]
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveAsync([FromRoute] int userId)
        {
            await _userService.RemoveUserByIdAsync(userId);

            return Ok();
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userInputModel"></param>
        /// <returns></returns>
        /// <response code="200">Returns when user is successfully updated</response>
        /// <response code="400">Returns when failed during user updating</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int userId, [FromBody] UserInputModel userInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _userService.UpdateUserAsync(userId, userInputModel);

            return Ok();
        }
    }
}