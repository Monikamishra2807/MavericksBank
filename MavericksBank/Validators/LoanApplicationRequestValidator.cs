using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class LoanApplicationRequestValidator : AbstractValidator<LoanApplicationDto>
    {
        public LoanApplicationRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Valid Customer Id is required.");

            RuleFor(x => x.LoanId)
                .GreaterThan(0)
                .WithMessage("Valid Loan Id is required.");

            RuleFor(x => x.RequestedAmount)
                .GreaterThan(0)
                .WithMessage("Requested Amount must be greater than zero.");

            RuleFor(x => x.Status)
                .Must(status =>
                    string.IsNullOrWhiteSpace(status) ||
                    status.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                    status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ||
                    status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Status must be Pending, Approved or Rejected.");
        }
    }
}