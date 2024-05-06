using FluentValidation;
using Models.Request;

namespace Domain.Validation
{
    public class CompanySearchRequestValidator : AbstractValidator<CompanySearchRequest>
    {
        public CompanySearchRequestValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
