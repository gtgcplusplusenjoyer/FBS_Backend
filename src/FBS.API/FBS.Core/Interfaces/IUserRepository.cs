using FBS.Core.Entities.User;

namespace FBS.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetUserByEmail(string email);
    }
}
