using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
using FBS.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _adminService.GetUsers();
            return Ok(users);
        }

        [HttpPut("users/{id}/role")]
        public async Task<ActionResult> ChangeUserRoleAsync(Guid id, [FromBody] ChangeRoleUserDto userRole)
        {
            if (!Enum.TryParse<RolesTypes>(userRole.role, true, out var role))
            {
                return BadRequest(new { message = "Incorrect role" });
            }

            if(role == RolesTypes.Admin)
            {
                return BadRequest(new { message = "Unavailable role" });
            }

            var result = await _adminService.ChangeUserRole(id, role);

            return Ok(new { success = result, message = "Роль успешно изменена" });
        }
    }
}