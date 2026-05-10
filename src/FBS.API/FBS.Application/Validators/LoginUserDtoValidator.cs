using FBS.Application.Dto.User;
using FluentValidation;

namespace FBS.Application.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Please, enter your email address")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Please, enter your password");
        }
    }
}
