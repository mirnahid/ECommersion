using ECommersionAPI.Application.Dtos.User;

namespace ECommersionAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
    }
}
