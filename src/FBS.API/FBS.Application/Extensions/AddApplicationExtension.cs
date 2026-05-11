using FBS.Application.Interfaces;
using FBS.Application.Mapper;
using FBS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FBS.Application.Extensions
{
    public static class AddApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(WorkoutMapper));
            services.AddService();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            return services;
        }
    }
}
