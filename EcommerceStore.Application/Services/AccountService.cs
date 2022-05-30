using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceStore.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TokenResponseModel> AuthenticateUserAccountAsync(UserLoginModel userLoginModel)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginModel.Email);

            if (!BCrypt.Net.BCrypt.Verify(userLoginModel.Password, user.PasswordHash))
                throw new ValidationException(ExceptionMessages.IncorrectUserCredentials);

            var tokenResponseModel = new TokenResponseModel
            {
                UserEmail = user.Email,
                UserId = user.Id,
                Role = user.Role.Name
            };

            return tokenResponseModel;
        }
    }
}
