using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Enter a valid email.");

            RuleFor(x => x.Mobile)
                .NotEmpty()
                .Matches(@"^[0-9]{10}$")
                .WithMessage("Mobile number must contain exactly 10 digits.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role =>
                    role.Equals("Customer", StringComparison.OrdinalIgnoreCase) ||
                    role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Role must be Customer or Admin.");
        }
    }
}