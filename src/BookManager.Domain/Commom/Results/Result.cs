using BookManager.Domain.Commom.Enums;

namespace BookManager.Domain.Commom.Results;
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Errors = error.Issue == Issues.none ? null : new() { error };
    }

    public Result(bool isSuccess, List<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public List<Error>? Errors { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    public static Result Failure(List<Error> errors) => new(false, errors);

    public static Result<T> Success<T>(T data) => new(true, Error.None, data);

    public static Result<T> Failure<T>(Error error) => new(false, error, default);
    public static Result<T> Failure<T>(List<Error> errors) => new(false, errors, default);
}
