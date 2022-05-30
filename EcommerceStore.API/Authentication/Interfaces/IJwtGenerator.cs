using EcommerceStore.Application.Models;

namespace EcommerceStore.API.Authentication.Interfaces
{
    public interface IJwtGenerator
    {
        public string GenerateJwtToken(UserResponseModel userResponseModel);
    }
}
