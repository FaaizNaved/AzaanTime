namespace NamazTimeApp.Transaction.Contracts;

public class PrayerPreferenceDto
{
    public bool Enabled { get; set; }
    public int Volume { get; set; }
}

public class DevicePrayerPreferencesDto
{
    public PrayerPreferenceDto Fajr { get; set; } = new();
    public PrayerPreferenceDto Sunrise { get; set; } = new();
    public PrayerPreferenceDto Dhuhr { get; set; } = new();
    public PrayerPreferenceDto Asr { get; set; } = new();
    public PrayerPreferenceDto Maghrib { get; set; } = new();
    public PrayerPreferenceDto Isha { get; set; } = new();
}
