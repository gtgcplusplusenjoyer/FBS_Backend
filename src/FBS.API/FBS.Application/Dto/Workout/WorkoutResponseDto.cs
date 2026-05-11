using FBS.Core.Entities.Training;
using FBS.Core.Enums;

namespace FBS.Application.Dto.Workout
{
    public record WorkoutResponseDto(
        Guid Id,
        Guid UserId,
        DateOnly Date,
        string Title,
        string? Notes,
        WorkoutType Type,
        List<Exercise>? Exercises,
        DateTime CreatedAt,
        DateTime UpdatedAt
        );
}
