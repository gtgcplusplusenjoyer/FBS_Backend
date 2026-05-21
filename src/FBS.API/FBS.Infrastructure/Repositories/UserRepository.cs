using FBS.Core.Entities.User;
using FBS.Core.Interfaces;
using FBS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FBS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FbsDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(FbsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _users = _context.Set<User>(); 
        }

        public async Task AddAsync(User user)
        { 
            await _users.AddAsync(user);
 
        }

        public async Task<User?> GetUserByEmail(string email)
        { 
            return await _users.FirstOrDefaultAsync(x=>x.Email== email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _users
                .AsNoTracking()
                .OrderBy(u=>u.Name)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _users.FirstAsync(x=>x.Id==id);
        }
    }
}