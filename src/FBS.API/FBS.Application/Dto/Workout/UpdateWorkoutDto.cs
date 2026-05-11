using FBS.Core.Enums;

namespace FBS.Application.Dto.Workout
{
    public record UpdateWorkoutDto(
        DateOnly Date,
        string Title,
        WorkoutType Type,
        string? Notes,
        List<ExerciseDto> Exercises
        );
}
