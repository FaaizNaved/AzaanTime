namespace NamazTimeApp.Transaction.Contracts;

public class DeviceRegistrationDto
{
    public Guid Id { get; set; }
    public string DeviceUniqueId { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public bool NotificationEnabled { get; set; }
    public DevicePrayerPreferencesDto? PrayerPreferences { get; set; }
}
