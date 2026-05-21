using AutoMapper;
using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
using FBS.Core.Enums;
using FBS.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FBS.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        public AdminService(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public async Task<bool> ChangeUserRole(Guid id, RolesTypes type)
        {
            var user = await _users.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentNullException("Unknown user");
            }

            if (type == RolesTypes.Trainer)
            {
                user.UserRole = type;
                await _users.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<UserDto>?> GetUsers()
        {
            var users = await _users.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
