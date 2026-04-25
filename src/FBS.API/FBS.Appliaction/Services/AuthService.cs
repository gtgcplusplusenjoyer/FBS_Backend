using FBS.Application.Dto;
using FBS.Application.Interfaces;
using FBS.Core.Entities.User;
using FBS.Core.Interfaces;
using FBS.Infrastructure.Context;
using FBS.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace FBS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly FbsDbContext _context;
        private readonly JwtService _jwtService;
        public AuthService(IUserRepository userRepository, FbsDbContext fbsDbContext, JwtService jwtService)
        {
            _context= fbsDbContext;
            _users = userRepository;
            _jwtService= jwtService;
        }

        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Email))
            {
                throw new ArgumentNullException(nameof(loginUserDto.Email));
            }

            var user = await _users.GetUserByEmail(loginUserDto.Email);

            if (user == null)
            {
                throw new Exception("User is not found");
            }

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Error login");
            }

            return _jwtService.GenerateToken(user);
             
        }

        public async Task<string> Register(RegisterUserDto registerUserDto)
        {
            var user = await _users.GetUserByEmail(registerUserDto.Email);
            if (user != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = registerUserDto.UserName,
                Email = registerUserDto.Email
            };
            var passHash = new PasswordHasher<User>().HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = passHash;

            await _users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return _jwtService.GenerateToken(newUser);
        }
    }
}
