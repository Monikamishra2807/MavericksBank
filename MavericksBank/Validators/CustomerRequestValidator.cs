using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class CustomerRequestValidator : AbstractValidator<CustomerDto>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Valid User Id is required.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(200);

            RuleFor(x => x.DOB)
                .LessThan(DateTime.Today)
                .WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.AadharNumber)
                .NotEmpty()
                .Matches(@"^[0-9]{12}$")
                .WithMessage("Aadhar Number must contain exactly 12 digits.");

            RuleFor(x => x.PanNumber)
                .NotEmpty()
                .Matches(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$")
                .WithMessage("Enter a valid PAN Number.");
        }
    }
}