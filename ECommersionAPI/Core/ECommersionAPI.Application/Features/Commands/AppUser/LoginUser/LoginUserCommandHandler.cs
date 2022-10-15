using ECommersionAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommersionAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 20);

            return new LoginUserSuccessCommandResponse()
            {
                AccessToken = token
            };
        }
    }
}
