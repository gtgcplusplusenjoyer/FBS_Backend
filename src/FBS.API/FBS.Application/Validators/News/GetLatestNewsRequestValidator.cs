using FBS.Application.Dto.News;
using FluentValidation;

namespace FBS.Application.Validators.News
{
    public class GetLatestNewsRequestValidator : AbstractValidator<GetLatestNewsRequest>
    {
        public GetLatestNewsRequestValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Количество новостей должно быть больше 0")
                .LessThanOrEqualTo(100).WithMessage("Количество новостей не может превышать 100");
        }
    }
}
