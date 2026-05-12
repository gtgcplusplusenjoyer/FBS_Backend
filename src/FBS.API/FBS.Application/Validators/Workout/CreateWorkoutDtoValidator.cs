using FBS.Application.Dto.Workout;
using FluentValidation;

namespace FBS.Application.Validators.Workout
{
    public class CreateWorkoutDtoValidator : AbstractValidator<CreateWorkoutDto>
    {
        private const int MaxPastMonths = 1;
        private const int MaxFutureMonths = 1;
        public CreateWorkoutDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");
             
            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-MaxPastMonths)))
                .WithMessage($"Cannot create workout earlier than {MaxPastMonths} month ago");
             
            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(MaxFutureMonths)))
                .WithMessage($"Cannot create workout more than {MaxFutureMonths} month in the future");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters")
                .MinimumLength(2).WithMessage("Title must be greater than 2 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid Workout type");

            RuleFor(x => x.Exercises)
                .NotNull().WithMessage("Exercises list cannot be null");

            RuleForEach(x => x.Exercises)
                .SetValidator(new ExerciseDtoValidator());
        }
    }
}
