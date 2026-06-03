using NamazTimeApp.Core;
using NamazTimeApp.Master;

namespace NamazTimeApp.Transaction;

public class DeviceRegistration : EntityBase
{
    public Guid Id { get; set; }

    public string DeviceUniqueId { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public Guid LocationId { get; set; }

    public string? CurrentFcmToken { get; set; }

    public string AppVersion { get; set; } = string.Empty;

    public bool NotificationEnabled { get; set; }

    /// <summary>JSON payload of per-prayer notification preferences (enabled, volume).</summary>
    public string? PrayerPreferencesJson { get; set; }

    public DateTime LastSeenOn { get; set; }

    public Location? Location { get; set; }
}
