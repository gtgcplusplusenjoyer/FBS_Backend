using FBS.Core.Interfaces;
using FBS.Infrastructure.Context;
using FBS.Infrastructure.Repositories;
using FBS.Infrastructure.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FBS.Application.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<FbsDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(FbsDbContext)));
            });

            services.AddScoped<IJwtService, JwtService>();

            return services;
        }


    }
}
