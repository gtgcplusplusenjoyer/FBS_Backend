using FBS.Core.Enums;

namespace FBS.Application.Dto.Workout
{
    public record CreateWorkoutDto(
        DateTime Date,
        string Title,
        WorkoutType Type,
        string? Notes,
        List<ExerciseDto> Exercises
        );
}
