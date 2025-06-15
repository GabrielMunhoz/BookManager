using BookManager.Domain.Commom.Enums;

namespace BookManager.Domain.Commom.Notifications;
public class Notification
{
    public Issues Issue { get; }
    public string Message { get; }

    public Notification(Issues key, string message)
    {
        Issue = key;
        Message = message;
    }
}
