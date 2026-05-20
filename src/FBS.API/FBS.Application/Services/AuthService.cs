using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
using FBS.Core.Entities.User;
using FBS.Core.Enums;
using FBS.Core.Interfaces;
using FBS.Core.Interfaces.External;

namespace FBS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        public AuthService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher passwordHasher)
        {
            _users = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
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

            bool isValid = _passwordHasher.VerifyPassword(loginUserDto.Password, user.PasswordHash);

            if (!isValid)
            {
                throw new Exception("Invalid password");
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
                Email = registerUserDto.Email,
                UserRole = RolesTypes.Visitor
            };

            var passHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = passHash;

            await _users.AddAsync(newUser);

            await _users.SaveChangesAsync();

            return _jwtService.GenerateToken(newUser);
        }
    }
}