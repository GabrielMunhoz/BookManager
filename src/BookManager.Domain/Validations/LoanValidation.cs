using BookManager.Domain.Entity;
using FluentValidation;

namespace BookManager.Domain.Validations;
public class LoanValidator : AbstractValidator<Loan>
{
    public LoanValidator()
    {

        RuleFor(loan => loan.ReturnDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("ReturnDate must be valid.");

        RuleFor(loan => loan.User)
            .NotNull()
            .WithMessage("User is required");

        RuleFor(loan => loan.Books)
            .NotNull().WithMessage("Books is required")
            .Must(books => books != null && books.Any())
            .WithMessage("Books is required");
    }
}
