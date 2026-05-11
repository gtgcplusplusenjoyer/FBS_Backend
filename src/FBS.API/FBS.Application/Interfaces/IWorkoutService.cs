using FBS.Application.Dto.Workout;

namespace FBS.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<WorkoutResponseDto>> GetWorkoutsByDateAsync(Guid userId, DateOnly date,CancellationToken cancellationToken);
        Task<WorkoutResponseDto?> GetWorkoutByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
        Task<WorkoutResponseDto> CreateWorkoutAsync(Guid userId, CreateWorkoutDto workout, CancellationToken cancellationToken);
        Task<bool> DeleteWorkoutAsync(Guid id, Guid userId, CancellationToken cancellationToken);
        Task<WorkoutResponseDto> UpdateWorkoutAsync(Guid id, Guid userId, UpdateWorkoutDto workout, CancellationToken cancellationToken);
    }
}