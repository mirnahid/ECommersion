using ECommersionAPI.Application.Abstractions.Services;
using ECommersionAPI.Application.Abstractions.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User = ECommersionAPI.Domain.Entities.Identity;

namespace ECommersionAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<User.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IAuthService _authService;

        public GoogleLoginCommandHandler(UserManager<User.AppUser> userManager, ITokenHandler tokenHandler, IAuthService authService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, 20);

            return new()
            {
                Token = token
            };
        }
    }
}
