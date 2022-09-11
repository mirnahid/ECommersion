using ECommersionAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = ECommersionAPI.Domain.Entities.Identity; 
namespace ECommersionAPI.Application.Features.Commands.AppUser.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<U.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<U.AppUser> userManager)
        {
            _userManager=userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result= await _userManager.CreateAsync(new()
            {
                Id=Guid.NewGuid().ToString(),
                UserName=request.UserName,
                Email=request.Email,
                NameSurname=request.NameSurname
            },request.Password);;

            if (result.Succeeded)
            {
                return new()
                {
                    Succeeded = true,
                    Message = "User successfully created"
                };
            }

            return new()
            {
                Succeeded = false,
                Message = "something went wrong :)"
            };
        }
    }
}
