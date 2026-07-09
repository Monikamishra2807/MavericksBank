using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class LoanRequestValidator : AbstractValidator<LoanDto>
    {
        public LoanRequestValidator()
        {
            RuleFor(x => x.LoanName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Loan Name is required.");

            RuleFor(x => x.InterestRate)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Interest Rate must be between 0 and 100.");

            RuleFor(x => x.TenureInMonths)
                .GreaterThan(0)
                .WithMessage("Tenure must be greater than 0 months.");

            RuleFor(x => x.MaximumAmount)
                .GreaterThan(0)
                .WithMessage("Maximum Loan Amount must be greater than 0.");
        }
    }
}