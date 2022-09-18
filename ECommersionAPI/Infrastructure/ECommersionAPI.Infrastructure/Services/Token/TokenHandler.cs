using ECommersionAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using T = ECommersionAPI.Application.Dtos;

namespace ECommersionAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T.Token CreateAccessToken(int minute)
         {
            T.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            JwtSecurityToken jwtSecurityToken = new(
                audience: _configuration["Token:Auidience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken= tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
