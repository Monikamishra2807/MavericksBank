using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}