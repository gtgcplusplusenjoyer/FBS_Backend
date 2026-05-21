using FBS.Core.Enums;

namespace FBS.Application.Dto.User
{
    public record ChangeRoleUserDto(Guid id, RolesTypes role);
}
