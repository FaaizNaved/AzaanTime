namespace NamazTimeApp.Master.Contracts;

public interface ILocationService
{
    /// <summary>
    /// Returns distinct country names from the Location table.
    /// </summary>
    Task<List<string>> GetDistinctCountriesAsync(CancellationToken ct = default);

    /// <summary>
    /// Returns distinct state names from the Location table.
    /// </summary>
    Task<List<string>> GetDistinctStatesAsync(CancellationToken ct = default);

    /// <summary>
    /// Returns distinct city (location) names from the Location table.
    /// </summary>
    Task<List<string>> GetDistinctCitiesAsync(CancellationToken ct = default);

    /// <summary>
    /// Resolves a location by country, state, and city name.
    /// </summary>
    Task<LocationDto?> ResolveLocationAsync(
        string country,
        string state,
        string city,
        CancellationToken ct = default);
}
