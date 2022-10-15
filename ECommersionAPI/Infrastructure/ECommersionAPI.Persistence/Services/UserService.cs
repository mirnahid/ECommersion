using ECommersionAPI.Application.Abstractions.Services;
using ECommersionAPI.Application.Dtos.User;
using ECommersionAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using U = ECommersionAPI.Domain.Entities.Identity;

namespace ECommersionAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<U.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                return new()
                {
                    Succeeded = true,
                    Message = "User successfully created"
                };
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            }

            return response;
        }
    }
}
