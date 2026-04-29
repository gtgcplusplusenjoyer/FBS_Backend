using FBS.Core.Entities.User;

namespace FBS.Core.Interfaces.External
{
    public interface IPasswordHasher
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
