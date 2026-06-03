namespace NamazTimeApp.Notification.Contracts;

public interface IAdhanNotificationDispatcher
{
    Task DispatchDueNotificationsAsync(CancellationToken ct = default);
}
