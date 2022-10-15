using ECommersionAPI.Application.Dtos;

namespace ECommersionAPI.Application.Abstractions.Services.Authentications
{
    public interface IInteralAuthentication
    {
        Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    }
}
