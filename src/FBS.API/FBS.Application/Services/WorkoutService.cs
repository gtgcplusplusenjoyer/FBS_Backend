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

        public async Task<List<WorkoutResponseDto>> GetWorkoutsByDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
        {
            var workouts = await _workouts.GetByUserIdAndDateAsync(userId, date);
            return workouts.Select(w => _mapper.Map<WorkoutResponseDto>(w)).ToList();
        }

        public async Task<WorkoutResponseDto> UpdateWorkoutAsync(Guid id, Guid userId, UpdateWorkoutDto workout, CancellationToken cancellationToken)
        {
            var newWorkout = await _workouts.GetByIdAsync(id);

            if (newWorkout == null || newWorkout.UserId != userId)
            {
                return null;
            }

            newWorkout.Date = workout.Date;
            newWorkout.Notes = workout.Notes;
            newWorkout.Title = workout.Title;
            newWorkout.Type = workout.Type;
            newWorkout.Exercises = WorkoutMapper.MapExercises(workout.Exercises);
            newWorkout.UpdatedAt = DateTime.UtcNow;

            _workouts.Update(newWorkout);
            await _workouts.SaveChangesAsync(cancellationToken);
            return _mapper.Map<WorkoutResponseDto>(newWorkout);
        }
    }
}
