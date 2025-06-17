namespace BookManager.Domain.Commom.Results;
public class PagedResult<T> : Result
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }

    public IEnumerable<T>? Values { get; set; }

    public PagedResult()
    {
        
    }

    public PagedResult(bool isSuccess, Error error, IEnumerable<T>? data) : base(isSuccess, error)
    {
        Values = data;
    }

    public PagedResult(bool isSuccess, IEnumerable<Error> errors, IEnumerable<T>? data) : base(isSuccess, errors)
    {
        Values = data;
    }

    public static PagedResult<TPaged> Success<TPaged>(IEnumerable<TPaged>? data) => new(true, Error.None, data);
    public static PagedResult<TPaged> Failure<TPaged>(IEnumerable<Error> errors) => new(false, errors, null);
}
