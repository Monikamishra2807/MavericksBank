using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class BeneficiaryRequestValidator : AbstractValidator<BeneficiaryDto>
    {
        public BeneficiaryRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Valid Customer Id is required.");

            RuleFor(x => x.BeneficiaryName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Beneficiary Name is required.");

            RuleFor(x => x.BankName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Bank Name is required.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .Length(10, 18)
                .WithMessage("Account Number must be between 10 and 18 digits.");

            RuleFor(x => x.IFSCCode)
                .NotEmpty()
                .Matches(@"^[A-Z]{4}0[A-Z0-9]{6}$")
                .WithMessage("Enter a valid IFSC Code.");
        }
    }
}