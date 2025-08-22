using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using FluentValidation.Results;

namespace BookManager.Domain.Extensions;
public static class ValidationResultExtensions
{
    public static Result<T> ToFailureResult<T>(this ValidationResult validationResult)
    {
        var errorMessages = validationResult.Errors
            .Select(e => new Error(Issues.e400, e.ErrorMessage));
        
        return Result.Failure<T>(errorMessages);
    }
    public static PagedResult<T> ToFailurePagedResult<T>(this ValidationResult validationResult)
    {
        var errorMessages = validationResult.Errors
            .Select(e => new Error(Issues.e400, e.ErrorMessage));
        
        return PagedResult<T>.Failure<T>(errorMessages);
    }
}