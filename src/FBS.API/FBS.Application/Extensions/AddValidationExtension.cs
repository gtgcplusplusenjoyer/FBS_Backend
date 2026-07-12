using FBS.Application.Dto.News;
using FBS.Application.Dto.User;
using FBS.Application.Dto.Workout;
using FBS.Application.Validators.News;
using FBS.Application.Validators.User;
using FBS.Application.Validators.Workout;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace FBS.Application.Extensions
{
    public static class AddValidationExtension
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<ExerciseDto>, ExerciseDtoValidator>();
            services.AddScoped<IValidator<CreateWorkoutDto>, CreateWorkoutDtoValidator>();
            services.AddScoped<IValidator<UpdateWorkoutDto>, UpdateWorkoutDtoValidator>();
            services.AddScoped<IValidator<GetLatestNewsRequest>, GetLatestNewsRequestValidator>();

            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
