using FBS.Core.Entities.User;

namespace FBS.Core.Interfaces.External
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
