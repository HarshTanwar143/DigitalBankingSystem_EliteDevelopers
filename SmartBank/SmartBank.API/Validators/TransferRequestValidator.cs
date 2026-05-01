using FluentValidation;
using SmartBank.Models.DTOs.Transactions;

namespace SmartBank.API.Validators;

public class TransferRequestValidator : AbstractValidator<TransferRequestDto>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.FromAccountId)
            .GreaterThan(0)
            .WithMessage("FromAccountId must be a valid positive integer.");

        RuleFor(x => x.ToAccountNumber)
            .NotEmpty()
            .WithMessage("Destination account number is required.")
            .MaximumLength(20)
            .WithMessage("Account number cannot exceed 20 characters.")
            .Matches(@"^[A-Za-z0-9]+$")
            .WithMessage("Account number must contain letters and digits only.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.")
            .LessThanOrEqualTo(10_000_000)
            .WithMessage("Amount cannot exceed 10,000,000.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500)
            .WithMessage("Remarks cannot exceed 500 characters.")
            .When(x => x.Remarks != null);
    }
}
