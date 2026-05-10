using FBS.Core.Entities.Training;

namespace FBS.Core.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<Workout?> GetByIdAsync(Guid id);
        Task<List<Workout>> GetByUserIdAndDateAsync(Guid userId, DateTime date);
        Task<List<Workout>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Workout workout);
        void Update(Workout workout);
        void Delete(Workout workout);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
