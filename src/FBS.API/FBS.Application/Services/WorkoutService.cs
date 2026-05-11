using AutoMapper;
using FBS.Application.Dto.Workout;
using FBS.Application.Interfaces;
using FBS.Application.Mapper;
using FBS.Core.Entities.Training;
using FBS.Core.Interfaces;

namespace FBS.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutRepository _workouts;

        public WorkoutService(IWorkoutRepository workouts, IMapper mapper)
        {
            _workouts = workouts;
            _mapper = mapper;
        }

        public async Task<WorkoutResponseDto> CreateWorkoutAsync(Guid userId, CreateWorkoutDto workout, CancellationToken cancellationToken)
        {
            var newWorkout = _mapper.Map<Workout>(workout);

            newWorkout.Date = workout.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            newWorkout.Id = Guid.NewGuid();
            newWorkout.UserId = userId;
            newWorkout.CreatedAt = DateTime.UtcNow;
            newWorkout.UpdatedAt = DateTime.UtcNow;


            await _workouts.AddAsync(newWorkout);
            await _workouts.SaveChangesAsync(cancellationToken);

            var responseWorkout = _mapper.Map<WorkoutResponseDto>(newWorkout);
            return responseWorkout;
        }

        public async Task<bool> DeleteWorkoutAsync(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var workout = await _workouts.GetByIdAsync(id);

            if (workout == null || workout.UserId != userId)
            {
                return false;
            }

            _workouts.Delete(workout);
            await _workouts.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<WorkoutResponseDto?> GetWorkoutByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var workout = await _workouts.GetByIdAsync(id);

            if (workout == null || workout.UserId != userId)
            {
                return null;
            }

            return _mapper.Map<WorkoutResponseDto>(workout);
        }

        public async Task<List<WorkoutResponseDto>> GetWorkoutsByDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken)
        { 
            var startDate = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var endDate = date.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);

            var workouts = await _workouts.GetByUserIdAndDateRangeAsync(userId, startDate, endDate, cancellationToken);
            return workouts.Select(w => _mapper.Map<WorkoutResponseDto>(w)).ToList();
        }

        public async Task<WorkoutResponseDto?> UpdateWorkoutAsync(Guid id, Guid userId, UpdateWorkoutDto workout, CancellationToken cancellationToken)
        {
            var existingWorkout = await _workouts.GetByIdAsync(id);

            if (existingWorkout == null || existingWorkout.UserId != userId)
            {
                return null;
            }
             
            existingWorkout.Date = workout.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            existingWorkout.Title = workout.Title;
            existingWorkout.Notes = workout.Notes;
            existingWorkout.Type = workout.Type;
            existingWorkout.Exercises = WorkoutMapper.MapExercises(workout.Exercises);
            existingWorkout.UpdatedAt = DateTime.UtcNow;

            _workouts.Update(existingWorkout);
            await _workouts.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkoutResponseDto>(existingWorkout);
        }
    }
}
