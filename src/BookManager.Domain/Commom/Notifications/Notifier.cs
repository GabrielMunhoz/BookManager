using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Interface.Common;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace BookManager.Domain.Commom.Notifications;
public class Notifier : INotifier
{
    private readonly List<Notification> _notifications;
    private readonly List<Notification> _errors;
    private readonly ILogger<Notifier> _logger;

    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public IReadOnlyCollection<Notification> Errors => _errors;
    public bool HasNotifications => _notifications.Count != 0;
    public bool HasErrors => _errors.Count != 0;

    public Notifier(ILogger<Notifier> logger)
    {
        _notifications = new List<Notification>();
        _errors = new List<Notification>();
        _logger = logger;
    }

    public void AddNotification(Issues issue, string message)
    {
        _notifications.Add(new Notification(issue, message));
        _logger.LogInformation($"Notification: {issue.ToString()} - {message}");
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
        _logger.LogInformation($"Notification: {notification.Issue} - {notification.Message}");
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
        _logger.LogInformation($"Notifications: {string.Join(",", notifications.Select(x => $"{x.Issue} - {x.Message}"))}");
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
        _logger.LogInformation($"Notifications: {string.Join(",", notifications.Select(x => $"{x.Issue} - {x.Message}"))}");
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
        _logger.LogInformation($"Notifications: {string.Join(",", notifications.Select(x => $"{x.Issue} - {x.Message}"))}");
    }

    public void AddErrors(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddError(Issues.e400, error.ErrorMessage);
            _logger.LogError($"Error: {Issues.e400} {error.ErrorCode} - {error.ErrorMessage}");
        }
    }

    public void AddError(Issues issue, string message)
    {
        _errors.Add(new Notification(issue, message));
        _logger.LogError($"Error: {issue.ToString()} - {message}");
    }
}