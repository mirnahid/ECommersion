using ECommersionAPI.Application.Abstractions.Services.Authentications;

namespace ECommersionAPI.Application.Abstractions.Services
{
    public interface IAuthService:IInteralAuthentication,IExternalAuthentication
    {
    }
}
