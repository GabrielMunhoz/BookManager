using BookManager.Domain.Commom.Enums;

namespace BookManager.Domain.Commom.Results;
public class Error
{
    public Error(Issues issue, string message)
    {
        Issue = issue;
        Message = message;
    }

    public string Message { get; }
    public Issues Issue { get; }

    public static Error None => new(Issues.none, string.Empty);

}
