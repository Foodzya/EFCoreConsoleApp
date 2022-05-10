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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserViewModel>> GetByIdAsync([FromRoute] int userId)
        {
            var userViewModel = await _userService.GetUserByIdAsync(userId);

            return Ok(userViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetAllAsync()
        {
            var usersViewModel = await _userService.GetAllUsersAsync();

            return Ok(usersViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UserInputModel userInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _userService.CreateUserAsync(userInputModel);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> RemoveAsync([FromRoute] int userId)
        {
            await _userService.RemoveUserByIdAsync(userId);

            return Ok();
        }

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