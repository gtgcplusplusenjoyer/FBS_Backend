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
            services.AddAutoMapper(cfg => { }, typeof(WorkoutMapper), typeof(UserMapper));
            services.AddService();
            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IChatAuthorizationService, ChatAuthorizationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}
