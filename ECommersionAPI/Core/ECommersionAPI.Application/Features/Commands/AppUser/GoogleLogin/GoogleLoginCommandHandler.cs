using ECommersionAPI.Application.Abstractions.Token;
using ECommersionAPI.Application.Dtos;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User = ECommersionAPI.Domain.Entities.Identity;

namespace ECommersionAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<User.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<User.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "fdsfsfsd" }
            };

            var payload= await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var info= new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            User.AppUser user=await _userManager.FindByLoginAsync(info.LoginProvider,info.ProviderKey);

            bool result = user != null;

            if (user==null)
            {
                user =await _userManager.FindByEmailAsync(payload.Email);

                if (user==null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email=payload.Email,
                        UserName=payload.Name,
                        NameSurname=payload.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);

                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);

            else
                throw new Exception("Error");

            Token token = _tokenHandler.CreateAccessToken(5);

            return new() {
                Token = token
            };
        }
    }
}
