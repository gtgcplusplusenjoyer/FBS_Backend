using FBS.Core.Entities.Training;
using FBS.Core.Interfaces;
using FBS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FBS.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FbsDbContext _context;
        private readonly DbSet<Workout> _workouts;

        public WorkoutRepository(FbsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _workouts = _context.Set<Workout>();
        }

        public async Task AddAsync(Workout workout)
        {
            await _workouts.AddAsync(workout);
        }

        public void Delete(Workout workout)
        {
            _workouts.Remove(workout);
        }

        public async Task<Workout?> GetByIdAsync(Guid id)
        {
            return await _workouts.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Workout>> GetByUserIdAndDateAsync(Guid userId, DateTime date)
        {
            return await _workouts.Where(w => w.UserId == userId && w.Date.Date == date)
                .OrderBy(w => w.Date)
                .ToListAsync();
        }

        public async Task<List<Workout>> GetByUserIdAsync(Guid userId)
        {
            return await _workouts.Where(w => w.UserId == userId)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Workout workout)
        {
            _workouts.Update(workout);
        }
    }
}
