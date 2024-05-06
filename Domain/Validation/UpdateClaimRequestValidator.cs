using FluentValidation;
using Models.Request;

namespace Domain.Validation
{
    public class UpdateClaimRequestValidator : AbstractValidator<UpdateClaimRequest>
    {
        public UpdateClaimRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Ucr)
                .NotEmpty();

            RuleFor(x => x.ClaimDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.LossDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.IncurredLoss)
                .NotEmpty()
                .GreaterThan(0m);

            RuleFor(x => x.AssuredName)
                .NotEmpty();
        }
    }
}
