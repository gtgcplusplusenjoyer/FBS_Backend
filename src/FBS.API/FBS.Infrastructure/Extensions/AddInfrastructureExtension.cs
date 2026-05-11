using FBS.Core.Interfaces;
using FBS.Core.Interfaces.External;
using FBS.Infrastructure.Context;
using FBS.Infrastructure.Repositories;
using FBS.Infrastructure.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql; 

namespace FBS.Application.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();

            var connectionString = configuration.GetConnectionString(nameof(FbsDbContext));
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.EnableDynamicJson();  
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<FbsDbContext>(options =>
            {
                options.UseNpgsql(dataSource);  
            });

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }


    }
}
