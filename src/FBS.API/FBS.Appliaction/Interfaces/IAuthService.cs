using FBS.Application.Dto;

namespace FBS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterUserDto registerUserDto);
        Task<string> Login(LoginUserDto loginUserDto);
    }
}
