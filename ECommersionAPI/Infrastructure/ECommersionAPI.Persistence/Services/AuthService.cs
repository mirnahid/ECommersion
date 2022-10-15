using ECommersionAPI.Application.Abstractions.Services;
using ECommersionAPI.Application.Abstractions.Token;
using ECommersionAPI.Application.Dtos;
using ECommersionAPI.Application.Exceptions;
using ECommersionAPI.Application.Features.Commands.AppUser.LoginUser;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = ECommersionAPI.Domain.Entities.Identity;

namespace ECommersionAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<U.AppUser> _userManager;
        private readonly SignInManager<U.AppUser> _signInManager;
        ITokenHandler _tokenHandler;

        public AuthService(IHttpClientFactory httpClientFactory, UserManager<U.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<U.AppUser> signInManager)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public Task FacebookLoginAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "fdsfsfsd" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            U.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Name,
                        NameSurname = payload.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);

                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);

            else
                throw new Exception("Error");

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
            return token;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            U.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;
            }

            throw new AuthenticationException();
        }

        public Task TwitterLoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
