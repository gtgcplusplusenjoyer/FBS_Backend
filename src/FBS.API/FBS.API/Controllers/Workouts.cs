using FBS.Application.Dto.Workout;
using FBS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _service;
        public WorkoutsController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkoutsByDate([FromQuery] DateOnly date,CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = Guid.Parse(userIdClaim);

            var workouts = await _service.GetWorkoutsByDateAsync(userId, date, cancellationToken);
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkoutsById(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = Guid.Parse(userIdClaim);

            var workout = await _service.GetWorkoutByIdAsync(id,userId,cancellationToken);

            if (workout == null)
                return NotFound(new { message = "Workout not found" });

            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutDto workoutDto, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = Guid.Parse(userIdClaim);

            if (workoutDto == null)
            {
                return BadRequest(new { message = "Workout data is required" });
            }
             
            var workout = await _service.CreateWorkoutAsync(userId, workoutDto, cancellationToken);

            return CreatedAtAction(nameof(GetWorkoutsById), new { id = workout.Id }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(Guid id,
            [FromBody] UpdateWorkoutDto workoutDto,
            CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = Guid.Parse(userIdClaim);

            var workout = await _service.UpdateWorkoutAsync(id,userId,workoutDto,cancellationToken);

            if (workout == null)
            {
                return NotFound(new { message = "Workout not found" });
            }

            return Ok(workout);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = Guid.Parse(userIdClaim);
            var workout = await _service.DeleteWorkoutAsync(id,userId,cancellationToken);

            if (!workout)
            {
                return NotFound(new { message = "Workout not found" });
            }

            return NoContent();
        }

    }
}
