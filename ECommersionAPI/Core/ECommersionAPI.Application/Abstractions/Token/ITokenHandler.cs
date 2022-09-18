using T = ECommersionAPI.Application.Dtos;

namespace ECommersionAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        T.Token CreateAccessToken(int minute);
    }
}
