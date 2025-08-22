using BookManager.Domain.Entity;
using FluentValidation;

namespace BookManager.Domain.Validations;
public class UserValidator : AbstractValidator<Users>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name required.");
        RuleFor(x => x.Email).NotEmpty();
    }
}
