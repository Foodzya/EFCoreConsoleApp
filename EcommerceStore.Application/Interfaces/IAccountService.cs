using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<UserResponseModel> AuthenticateUserAccountAsync(UserLoginModel userLoginModel);
    }
}
