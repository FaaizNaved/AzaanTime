using NamazTimeApp.Core;

namespace NamazTimeApp.Transaction.Contracts;

public interface IPrayerTimeService
{
    /// <summary>
    /// Generates prayer timings for the entire current year (365/366 days) for the given location.
    /// Fetches location coordinates from the database and uses the Adhan library for calculation.
    /// </summary>
    /// <param name="locationCode">The unique code of the location (e.g., "KANPUR").</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A Message containing the generation result.</returns>
    Task<Message> GenerateYearlyPrayerTimesAsync(string locationCode, CancellationToken ct = default);

    /// <summary>
    /// Returns prayer times for today for the given location code.
    /// </summary>
    Task<(TodayPrayerTimesDto? Model, Message Message)> GetTodayPrayerTimesAsync(
        string locationCode,
        CancellationToken ct = default);
}
