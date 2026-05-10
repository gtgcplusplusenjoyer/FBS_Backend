namespace FBS.Application.Dto.Workout
{
    public record ExerciseDto(
        string Name,
        int? Sets,
        int? Reps
        );
}
