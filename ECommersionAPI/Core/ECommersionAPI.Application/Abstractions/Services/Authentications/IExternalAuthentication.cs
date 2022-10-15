namespace ECommersionAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task FacebookLoginAsync();
        Task<Dtos.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
        Task TwitterLoginAsync();
    }
}
