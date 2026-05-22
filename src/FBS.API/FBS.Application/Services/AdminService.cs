using AutoMapper;
using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
using FBS.Core.Enums;
using FBS.Core.Interfaces;

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
                throw new ArgumentNullException(nameof(id), "Пользователь не найден");

            if (user.UserRole == RolesTypes.Admin && type != RolesTypes.Admin)
            {
                throw new InvalidOperationException("Нельзя изменить роль администратора");
            }

            user.UserRole = type;
            await _users.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserDto>?> GetUsers()
        {
            var users = await _users.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
