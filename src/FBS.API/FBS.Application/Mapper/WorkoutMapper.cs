using AutoMapper;
using FBS.Application.Dto.Workout;
using FBS.Core.Entities.Training;

namespace FBS.Application.Mapper
{
    public class WorkoutMapper : Profile
    {
        public WorkoutMapper()
        {
            CreateMap<DateOnly, DateTime>().ConvertUsing(src => src.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
            CreateMap<DateTime, DateOnly>().ConvertUsing(src => DateOnly.FromDateTime(src));

            CreateMap<CreateWorkoutDto, Workout>().
                ForMember(w => w.Exercises, opt => opt.MapFrom(src => MapExercises(src.Exercises)));

            CreateMap<Workout, WorkoutResponseDto>().
                ForMember(w => w.Exercises, opt => opt.MapFrom(src => MapExerciseDtos(src.Exercises)));

            CreateMap<UpdateWorkoutDto, Workout>().
                ForMember(w => w.Exercises, opt => opt.MapFrom(stc => MapExercises(stc.Exercises)));

            CreateMap<Exercise, ExerciseDto>().ReverseMap();
        }


        public static List<Exercise> MapExercises(List<ExerciseDto>? dtos)
        {
            return dtos?.Select(e => new Exercise
            {
                Name = e.Name,
                Reps = e.Reps,
                Sets = e.Sets
            }).ToList() ?? new();
        }
        public static List<ExerciseDto> MapExerciseDtos(List<Exercise>? exercises)
        {
            return exercises?.Select(e => new ExerciseDto(
                e.Name,
                e.Sets,
                e.Reps
            )).ToList() ?? new();
        }
    }
}
