using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Entity;
using FluentValidation;

namespace BookManager.Domain.Validations;
public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.ISBN)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        RuleFor(x => x.Autor)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        RuleFor(x => x.ReleaseYear)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithErrorCode(Issues.e400.ToString());

    }
}
