using FBS.Application.Dto.User;
using FBS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace FBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var token = await _authService.Register(registerUserDto);
                return Ok(new { token, message = "Register successful" });
            }

            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
        {

            try
            {
                var token = await _authService.Login(loginUserDto);
                return Ok(new { token, message = "Login successful" });
            }

            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }

        }

    }
}
