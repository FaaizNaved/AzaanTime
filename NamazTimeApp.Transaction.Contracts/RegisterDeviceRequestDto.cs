namespace NamazTimeApp.Transaction.Contracts;

public class RegisterDeviceRequestDto
{
    public string DeviceUniqueId { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public string? FcmToken { get; set; }
    public string AppVersion { get; set; } = "1.0.0";
    public bool NotificationEnabled { get; set; } = true;
    public DevicePrayerPreferencesDto? PrayerPreferences { get; set; }
}
