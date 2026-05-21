using FBS.Application.Dto.User;
using FBS.Core.Enums;

namespace FBS.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetUsers();
        Task<bool> ChangeUserRole(Guid id, RolesTypes type);
    }
}
