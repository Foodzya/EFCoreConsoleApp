using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserInputModel userInputModel)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userInputModel.Email);

            if (existingUser != null)
                throw new ValidationException(ExceptionMessages.UserAlreadyExists);

            var user = new User
            {
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                Email = userInputModel.Email,
                PhoneNumber = userInputModel.PhoneNumber,
                RoleId = userInputModel.RoleId
            };

            await _userRepository.CreateAsync(user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users is null)
                throw new ValidationException(ExceptionMessages.UserNotFound);

            var usersViewModel = users
                .Select(u => new UserViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role.Name
                })
                .ToList();

            return usersViewModel;
        }

        public async Task<UserViewModel> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new ValidationException(ExceptionMessages.UserNotFound, userId);

            var userViewModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.Name
            };

            return userViewModel;
        }

        public async Task RemoveUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new ValidationException(ExceptionMessages.UserNotFound, userId);

            _userRepository.Remove(user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int userId, UserInputModel userInputModel)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userInputModel.Email);

            if (existingUser != null && existingUser.Id != userId)
                throw new ValidationException(ExceptionMessages.UserAlreadyExists, userId);

            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new ValidationException(ExceptionMessages.UserNotFound, userId);

            user.FirstName = userInputModel.FirstName;
            user.LastName = userInputModel.LastName;
            user.PhoneNumber = userInputModel.PhoneNumber;
            user.Email = userInputModel.Email;
            user.RoleId = userInputModel.RoleId;

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();
        }
    }
}