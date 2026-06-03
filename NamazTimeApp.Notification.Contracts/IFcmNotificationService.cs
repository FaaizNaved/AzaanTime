namespace NamazTimeApp.Notification.Contracts;

public interface IFcmNotificationService
{
    Task<bool> SendAdhanNotificationAsync(
        string fcmToken,
        string prayerCode,
        string prayerName,
        string title,
        string body,
        string soundFile,
        int volume,
        CancellationToken ct = default);
}
