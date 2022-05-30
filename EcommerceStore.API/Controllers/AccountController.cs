using EcommerceStore.API.Authentication.Interfaces;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountController(IAccountService accountService, IJwtGenerator jwtGenerator)
        {
            _accountService = accountService;
            _jwtGenerator = jwtGenerator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> SignInAsync([FromBody] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ExceptionMessages.IncorrectUserCredentials);

            var userResponseModel = await _accountService.AuthenticateUserAccountAsync(userLoginModel);

            var accessToken = _jwtGenerator.GenerateJwtToken(userResponseModel);

            var tokenResponseModel = new TokenResponseViewModel
            {
                UserEmail = userResponseModel.Email,
                Role = userResponseModel.Role,
                UserId = userResponseModel.Id,
                AccessToken = accessToken
            };

            return Ok(tokenResponseModel);
        }
    }
}