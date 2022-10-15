using ECommersionAPI.Application.Dtos;

namespace ECommersionAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
       
    }

    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token AccessToken { get; set; }
    }

    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
