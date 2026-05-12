using FBS.Application.Dto.Workout;
using FluentValidation;

namespace FBS.Application.Validators.Workout
{
    public class ExerciseDtoValidator : AbstractValidator<ExerciseDto>
    {
        public ExerciseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exercise name is required")
                .MaximumLength(100).WithMessage("Exercise name must not exceed 100 characters");

            RuleFor(x => x.Sets)
                .GreaterThan(0).When(x => x.Sets.HasValue)
                .WithMessage("Sets must be greater than 0");

            RuleFor(x => x.Reps)
                .GreaterThan(0).When(x => x.Reps.HasValue)
                .WithMessage("Reps must be greater than 0");
        }
    }
}
