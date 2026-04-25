using FBS.Core.Entities.User;

namespace FBS.Core.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
