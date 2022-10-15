using ECommersionAPI.Application.Abstractions.Services;
using ECommersionAPI.Application.Dtos.User;
using MediatR;
namespace ECommersionAPI.Application.Features.Commands.AppUser.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Username = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.ConfirmPassword
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
