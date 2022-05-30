using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceStore.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<TokenResponseModel> AuthenticateUserAccountAsync(UserLoginModel userLoginModel);
    }
}
