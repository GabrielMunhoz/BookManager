using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Notifications;
using FluentValidation.Results;

namespace BookManager.Domain.Interface.Common;
public interface INotifier
{
    bool HasNotifications { get; }
    bool HasErrors { get; }

    IReadOnlyCollection<Notification> Notifications { get; }
    IReadOnlyCollection<Notification> Errors { get; }

    void AddNotification(Issues issue, string message);
    void AddNotification(Notification notification);
    void AddNotifications(IReadOnlyCollection<Notification> notifications);
    void AddNotifications(IList<Notification> notifications);
    void AddNotifications(ICollection<Notification> notifications);
    void AddErrors(ValidationResult validationResult);
    void AddError(Issues issue, string message);
}