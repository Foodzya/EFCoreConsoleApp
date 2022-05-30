using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserViewModel> GetUserByIdAsync(int userId);
        public Task<List<UserViewModel>> GetAllUsersAsync();
        public Task CreateUserAsync(UserInputModel userInputModel);
        public Task RemoveUserByIdAsync(int userId);
        public Task UpdateUserAsync(int userId, UserInputModel userInputModel);
    }
}