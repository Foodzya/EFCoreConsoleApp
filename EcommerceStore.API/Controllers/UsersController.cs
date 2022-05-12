using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when user is successfully obtained</response>
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
        [HttpDelete("{userId}")]
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
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int userId, [FromBody] UserInputModel userInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _userService.UpdateUserAsync(userId, userInputModel);

            return Ok();
        }
    }
}