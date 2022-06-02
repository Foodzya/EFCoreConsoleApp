using EcommerceStore.API.Authentication.Interfaces;
using EcommerceStore.Application.Models;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceStore.API.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IOptions<JwtConfig> _jwtConfigOptions;

        public JwtGenerator(IOptions<JwtConfig> jwtConfigOptions)
        {
            _jwtConfigOptions = jwtConfigOptions;
        }

        public string GenerateJwtToken(UserResponseModel userResponseModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfigOptions.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userResponseModel.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, userResponseModel.Role),
                    new Claim(JwtRegisteredClaimNames.Email, userResponseModel.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfigOptions.Value.Audience,
                Issuer = _jwtConfigOptions.Value.Issuer
            };

            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return token;
        }
    }
}