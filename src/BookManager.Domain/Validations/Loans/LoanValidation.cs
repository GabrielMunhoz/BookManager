using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using FluentValidation;

namespace BookManager.Domain.Validations.Loans;
public class LoanValidator : AbstractValidator<Loan>
{
    public LoanValidator(IBookRepository bookRepository, IUserRepository userRepository)
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

        RuleForEach(loan => loan.Books)
           .MustAsync(async (book, cancellation) => await bookRepository.GetByIdAsync(book.Id) is not null)
           .WithMessage("Validation of existing books failed.")
           .WithErrorCode(Issues.e1001.ToString());

        RuleFor(loan => loan.User)
           .MustAsync(async (user, cancellation) => await userRepository.GetByIdAsync(user.Id) is not null)
           .WithMessage("Validation of existing user failed.")
           .WithErrorCode(Issues.e1002.ToString());

        RuleForEach(loan => loan.Books)
           .MustAsync(async (book, cancellation) => await bookRepository.IsInStockAsync(book.Id))
           .WithMessage("Book '{PropertyValue.Title}' is not in stock.")
           .WithErrorCode(Issues.e1014.ToString());
    }
}
