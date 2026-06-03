using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Master.Contracts;

namespace NamazTimeApp.Master.Service;

public class LocationService : ILocationService
{
    private readonly IAppUnitOfWork<AppDbContext> _unitOfWork;

    public LocationService(IAppUnitOfWork<AppDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<string>> GetDistinctCountriesAsync(CancellationToken ct = default)
    {
        return await _unitOfWork.Locations
            .Query()
            .Where(l => l.IS_ACTIVE)
            .Select(l => l.CountryName)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<List<string>> GetDistinctStatesAsync(CancellationToken ct = default)
    {
        return await _unitOfWork.Locations
            .Query()
            .Where(l => l.IS_ACTIVE)
            .Select(l => l.StateName)
            .Distinct()
            .OrderBy(s => s)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<List<string>> GetDistinctCitiesAsync(CancellationToken ct = default)
    {
        return await _unitOfWork.Locations
            .Query()
            .Where(l => l.IS_ACTIVE)
            .Select(l => l.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<LocationDto?> ResolveLocationAsync(
        string country,
        string state,
        string city,
        CancellationToken ct = default)
    {
        var location = await _unitOfWork.Locations
            .Query()
            .Where(l => l.IS_ACTIVE
                        && l.CountryName == country
                        && l.StateName == state
                        && l.Name == city)
            .Select(l => new LocationDto
            {
                Id = l.Id,
                Code = l.Code,
                Name = l.Name,
                StateName = l.StateName,
                CountryName = l.CountryName,
                TimeZone = l.TimeZone
            })
            .FirstOrDefaultAsync(ct);

        return location;
    }
}
