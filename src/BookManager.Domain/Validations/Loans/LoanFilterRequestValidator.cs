using BookManager.Domain.Model.Loans;
using FluentValidation;

namespace BookManager.Domain.Validations.Loans;
public class LoanFilterRequestValidator : AbstractValidator<LoanFilterRequest>
{
    public LoanFilterRequestValidator()
    {
        RuleFor(x => x.InitialReturnDate)
            .NotEmpty()
            .Must(x => x != DateTime.MinValue)
            .WithMessage("{PropertyName} Must be valid.")
            .When(x => x.FinalReturnDate.HasValue)
            .WithMessage("Both InitialReturnDate and FinalReturnDate must be provided together.");

        RuleFor(x => x.FinalReturnDate)
            .NotEmpty()
            .Must(x => x != DateTime.MinValue)
            .WithMessage("{PropertyName} Must be valid.")
            .When(x => x.InitialReturnDate.HasValue)
            .WithMessage("Both InitialReturnDate and FinalReturnDate must be provided together.");

        RuleFor(x => x.InitialCreateDate)
            .NotEmpty()
            .Must(x => x != DateTime.MinValue)
            .WithMessage("{PropertyName} Must be valid.")
            .When(x => x.FinalCreateDate.HasValue)
            .WithMessage("Both InitialCreateDate and FinalCreateDate must be provided together.");

        RuleFor(x => x.FinalCreateDate)
            .NotEmpty()
            .Must(x => x != DateTime.MinValue)
            .WithMessage("{PropertyName} Must be valid.")
            .When(x => x.InitialCreateDate.HasValue)
            .WithMessage("Both InitialCreateDate and FinalCreateDate must be provided together.");
    }
}
