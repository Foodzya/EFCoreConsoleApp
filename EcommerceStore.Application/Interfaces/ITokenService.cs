using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
    }
}