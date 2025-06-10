namespace BookManager.Domain.Commom.Results;
public class Result<T> : Result
{
    public T? Data { get; }

    public Result(bool isSuccess, Error error, T? data) : base(isSuccess, error)
    {
        Data = data;
    }
    
    public Result(bool isSuccess, List<Error> errors, T? data) : base(isSuccess, errors)
    {
        Data = data;
    }
}
