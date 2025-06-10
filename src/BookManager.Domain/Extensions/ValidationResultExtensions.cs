using BookManager.Domain.Commom.Results;
using FluentValidation.Results;

namespace BookManager.Domain.Extensions;
public static class ValidationResultExtensions
{
    public static Result<T> ToFailureResult<T>(this ValidationResult validationResult)
    {
        var errorMessages = validationResult.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

        var errorMessage = string.Join("; ", errorMessages);

        return Result.Failure<T>(new Error(errorMessage));
    }
}