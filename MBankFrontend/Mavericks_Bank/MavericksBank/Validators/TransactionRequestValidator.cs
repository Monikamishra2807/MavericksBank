using FluentValidation;
using MavericksBank.DTOs;

namespace MavericksBank.Validators
{
    public class TransactionRequestValidator : AbstractValidator<TransactionDto>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.FromAccountId)
                .GreaterThan(0)
                .WithMessage("Valid From Account Id is required.");

            RuleFor(x => x.ToAccountId)
                .GreaterThan(0)
                .WithMessage("Valid To Account Id is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Transaction amount must be greater than zero.");

            RuleFor(x => x.TransactionType)
                .NotEmpty()
                .Must(type =>
                    type.Equals("Transfer", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Deposit", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Withdrawal", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Transaction Type must be Transfer, Deposit or Withdrawal.");
        }
    }
}