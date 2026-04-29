using FBS.Core.Entities.User;
using FBS.Core.Interfaces.External;

namespace FBS.Infrastructure.Services.External
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
            return hash;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
