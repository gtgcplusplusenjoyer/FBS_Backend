using FBS.Core.Enums;

namespace FBS.Application.Dto.Workout
{
    public record UpdateWorkoutDto(
        DateTime Date,
        string Title,
        WorkoutType Type,
        string? Notes,
        List<ExerciseDto> Exercises
        );
}
