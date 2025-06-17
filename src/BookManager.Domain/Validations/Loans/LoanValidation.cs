using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Entity;
using FluentValidation;

namespace BookManager.Domain.Validations.Loans;
public class LoanValidator : AbstractValidator<Loan>
{
    public LoanValidator()
    {

        RuleFor(loan => loan.ReturnDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("{PropertyName} must be valid.")
            .WithErrorCode(Issues.e400.ToString());

        RuleFor(loan => loan.User)
            .NotNull()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());

        RuleFor(loan => loan.Books)
            .NotNull()
            .Must(books => books.Count > 0)
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        
        RuleFor(loan => loan.Status)
            .NotNull()
            .IsInEnum()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
    }
}
