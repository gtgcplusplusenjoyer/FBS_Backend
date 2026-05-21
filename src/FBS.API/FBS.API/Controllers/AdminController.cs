using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
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

        [HttpGet("Users")]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _adminService.GetUsers(); 
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeUserRoleAsync([FromBody] ChangeRoleUserDto changeDto)
        {
            var user = await _adminService.ChangeUserRole(changeDto.id, changeDto.role);

            return Ok(user);
        } 

    }
}
