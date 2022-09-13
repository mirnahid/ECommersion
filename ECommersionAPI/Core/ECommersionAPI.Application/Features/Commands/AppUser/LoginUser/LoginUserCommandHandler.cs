using ECommersionAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = ECommersionAPI.Domain.Entities.Identity;

namespace ECommersionAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<U.AppUser> _userManager;
        private readonly SignInManager<U.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<U.AppUser> userManager, SignInManager<U.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            U.AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
            if (result.Succeeded)
                return new();

            return new();
        }
    }
}
