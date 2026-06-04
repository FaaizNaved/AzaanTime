namespace NamazTimeApp.Transaction.Contracts;

public class PrayerDayTimesDto
{
    public string LocationCode { get; set; } = string.Empty;
    public DateOnly PrayerDate { get; set; }
    public string Fajr { get; set; } = string.Empty;
    public string Sunrise { get; set; } = string.Empty;
    public string Dhuhr { get; set; } = string.Empty;
    public string Asr { get; set; } = string.Empty;
    public string Maghrib { get; set; } = string.Empty;
    public string Isha { get; set; } = string.Empty;
}
