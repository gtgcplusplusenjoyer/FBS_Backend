using Microsoft.OpenApi.Models;

namespace FBS.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerWithJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                 new OpenApiSecurityScheme
                        {
                     Reference = new OpenApiReference
                            {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                            }
                        },
                 Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
