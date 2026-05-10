using FBS.Application.Dto.User;
using FBS.Application.Validators;
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
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
