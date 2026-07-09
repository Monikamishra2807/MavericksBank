using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class AccountRequestValidator : AbstractValidator<AccountDto>
    {
        public AccountRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Valid Customer Id is required.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .Length(10, 18)
                .WithMessage("Account Number must be between 10 and 18 digits.");

            RuleFor(x => x.AccountType)
                .NotEmpty()
                .Must(type =>
                    type.Equals("Savings", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Current", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Account Type must be Savings or Current.");

            RuleFor(x => x.IFSCCode)
                .NotEmpty()
                .Matches(@"^[A-Z]{4}0[A-Z0-9]{6}$")
                .WithMessage("Enter a valid IFSC Code.");

            RuleFor(x => x.BranchName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Branch Name is required.");

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance cannot be negative.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(status =>
                    status.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                    status.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Status must be Active or Inactive.");
        }
    }
}