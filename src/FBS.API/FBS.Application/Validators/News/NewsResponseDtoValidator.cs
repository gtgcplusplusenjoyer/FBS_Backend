using FBS.Application.Dto.News;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBS.Application.Validators.News
{
    public class NewsResponseDtoValidator : AbstractValidator<NewsResponseDto>
    {
        public NewsResponseDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("The news ID is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The headline of the news is required")
                .MaximumLength(500).WithMessage("The title should not exceed 500 characters");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("The link to the image is required")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("The link to the image must be a valid URL.");

            RuleFor(x => x.PublishedAt)
                .NotEmpty().WithMessage("The publication date is required")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("The publication date cannot be in the future");
        }
    }
}
