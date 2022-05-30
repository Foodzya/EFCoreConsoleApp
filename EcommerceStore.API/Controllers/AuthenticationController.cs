using EcommerceStore.API.Authentication;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IOptions<JwtConfig> _jwtConfigOptions;

        public AuthenticationController(IAccountService accountService, IOptions<JwtConfig> jwtConfigOptions)
        {
            _accountService = accountService;
            _jwtConfigOptions = jwtConfigOptions;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> AuthenticateAsync([FromBody] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ExceptionMessages.IncorrectUserCredentials);

            var tokenResponseModel = await _accountService.AuthenticateUserAccountAsync(userLoginModel);

            JwtGenerator jwtGenerator = new JwtGenerator(_jwtConfigOptions);

            tokenResponseModel.Token = jwtGenerator.GenerateJwtToken(tokenResponseModel);

            return Ok(tokenResponseModel);
        }
    }
}