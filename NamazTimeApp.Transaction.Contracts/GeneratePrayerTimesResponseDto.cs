namespace NamazTimeApp.Transaction.Contracts;

public class GeneratePrayerTimesResponseDto
{
    public int TotalDaysGenerated { get; set; }

    public string LocationName { get; set; } = string.Empty;

    public int Year { get; set; }
}
