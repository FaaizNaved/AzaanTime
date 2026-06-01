using NamazTimeApp.Core;
using NamazTimeApp.Master;
using NamazTimeApp.Transaction;

namespace NamazTimeApp.Notification;

public class NotificationLog : EntityBase
{
    public Guid Id { get; set; }

    public Guid DeviceRegistrationId { get; set; }

    public Guid PrayerTypeId { get; set; }

    public DateOnly PrayerDate { get; set; }

    public string NotificationTitle { get; set; } = string.Empty;

    public string? NotificationBody { get; set; }

    public DateTime SentOn { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? FailureReason { get; set; }

    public DeviceRegistration? DeviceRegistration { get; set; }

    public PrayerType? PrayerType { get; set; }
}
