using FBS.Core.Interfaces.External;
using FBS.Infrastructure.Services.External;
using FBS.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FBS.Application.Extensions
{
    public static class AddAuthenticationAndJwtExtension
    {
        public static IServiceCollection AddAuthenticationAndJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            services.AddAuth(configuration);

            return services;
        }
    }
}
