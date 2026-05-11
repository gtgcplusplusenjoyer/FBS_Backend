using FBS.Core.Enums;

namespace FBS.Application.Dto.Workout
{
    public record CreateWorkoutDto(
        DateOnly Date,
        string Title,
        WorkoutType Type,
        string? Notes,
        List<ExerciseDto> Exercises
        );
}
