using FluentValidation;
using Models.Request;

namespace Domain.Validation
{
    public class ClaimSearchRequestValidator : AbstractValidator<ClaimSearchRequest>
    {
        public ClaimSearchRequestValidator()
        {
            RuleFor(x => x.ClaimId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
