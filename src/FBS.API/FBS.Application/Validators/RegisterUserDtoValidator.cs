using FBS.Application.Dto;
using FluentValidation;

namespace FBS.Application.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("The password must contain a uppercase letter")
                .Matches(@"[a-z]").WithMessage("The password must contain a lowercase letter")
                .Matches(@"\d").WithMessage("The password must contain a number.")
                .Matches(@"[!@#$%^&*()]").WithMessage("The password must contain a special character");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MaximumLength(100);

        }
    }
}
