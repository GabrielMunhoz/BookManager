using BookManager.Domain.Interface.Common;
using FluentValidation.Results;

namespace BookManager.Domain.Commom.Notifications;
public class Notifier: INotifier
{
    private readonly List<Notification> _notifications;
    private readonly List<Notification> _errors;
    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public IReadOnlyCollection<Notification> Errors => _errors;
    public bool HasNotifications => _notifications.Count != 0;
    public bool HasErrors => _errors.Count != 0;

    public Notifier()
    {
        _notifications = new List<Notification>();
        _errors = new List<Notification>();
    }

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddErrors(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddError(error.ErrorCode, error.ErrorMessage);
        }
    }

    public void AddError(string key, string message)
    {
        _errors.Add(new Notification(key, message));
    }

}