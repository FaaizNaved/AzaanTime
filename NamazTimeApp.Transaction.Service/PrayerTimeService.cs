using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NamazTimeApp.Core;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Transaction.Contracts;

using AdhanPrayerTimes = Batoulapps.Adhan.PrayerTimes;

namespace NamazTimeApp.Transaction.Service;

public class PrayerTimeService : IPrayerTimeService
{
    private readonly IAppUnitOfWork<AppDbContext> _unitOfWork;
    private readonly ILogger<PrayerTimeService> _logger;

    public PrayerTimeService(
        IAppUnitOfWork<AppDbContext> unitOfWork,
        ILogger<PrayerTimeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Message> GenerateYearlyPrayerTimesAsync(
        string locationCode,
        CancellationToken ct = default)
    {
        // 1. Fetch location from database
        var location = await _unitOfWork.Locations
            .Query()
            .FirstOrDefaultAsync(l => l.Code == locationCode && l.IS_ACTIVE, ct);

        if (location == null)
        {
            throw new DomainException(
                MessageFactory.CreateErrorMessage(
                    string.Format(Constants.ApplicationMessages.NOT_FOUND, "Location")));
        }

        _logger.LogInformation(
            "Generating prayer times for {LocationName} (Lat: {Lat}, Lng: {Lng}, TZ: {TZ})",
            location.Name, location.Latitude, location.Longitude, location.TimeZone);

        // 2. Set up Adhan coordinates and calculation parameters
        var coordinates = new Coordinates(
            (double)location.Latitude,
            (double)location.Longitude);

        var calculationParameters = CalculationMethod.KARACHI.GetParameters();
        calculationParameters.Madhab = Madhab.HANAFI;

        // 3. Determine the year range
        var currentYear = DateTime.UtcNow.Year;
        var startDate = new DateOnly(currentYear, 1, 1);
        var endDate = new DateOnly(currentYear, 12, 31);
        var totalDays = endDate.DayNumber - startDate.DayNumber + 1;

        // 4. Remove any existing prayer times for this location and year
        var existingRecords = await _unitOfWork.PrayerTimes
            .Query()
            .Where(pt => pt.LocationId == location.Id
                         && pt.PrayerDate >= startDate
                         && pt.PrayerDate <= endDate)
            .ToListAsync(ct);

        if (existingRecords.Count > 0)
        {
            _logger.LogInformation(
                "Removing {Count} existing prayer time records for {Location} in {Year}",
                existingRecords.Count, location.Name, currentYear);

            foreach (var record in existingRecords)
            {
                _unitOfWork.PrayerTimes.Delete(record);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }

        // 5. Generate prayer times for each day of the year
        var locationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(location.TimeZone);
        var prayerTimeEntities = new List<PrayerTime>(totalDays);

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var dateTime = date.ToDateTime(TimeOnly.MinValue);
            var dateComponents = DateComponents.From(dateTime);

            var prayerTimes = new AdhanPrayerTimes(coordinates, dateComponents, calculationParameters);

            // Convert UTC DateTime values to the location's local timezone
            var fajrLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Fajr, locationTimeZone);
            var sunriseLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Sunrise, locationTimeZone);
            var dhuhrLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Dhuhr, locationTimeZone);
            var asrLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Asr, locationTimeZone);
            var maghribLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Maghrib, locationTimeZone);
            var ishaLocal = TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Isha, locationTimeZone);

            var entity = new PrayerTime
            {
                Id = Guid.NewGuid(),
                LocationId = location.Id,
                PrayerDate = date,
                Fajr = TimeOnly.FromDateTime(fajrLocal),
                Sunrise = TimeOnly.FromDateTime(sunriseLocal),
                Dhuhr = TimeOnly.FromDateTime(dhuhrLocal),
                Asr = TimeOnly.FromDateTime(asrLocal),
                Maghrib = TimeOnly.FromDateTime(maghribLocal),
                Isha = TimeOnly.FromDateTime(ishaLocal),
                CREATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID,
                RECORD_SOURCE_NAME = Constants.Common.RECORD_SOURCE
            };

            prayerTimeEntities.Add(entity);
        }

        // 6. Bulk insert all prayer times
        await _unitOfWork.PrayerTimes.AddRangeAsync(prayerTimeEntities, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation(
            "Successfully generated {Count} prayer time records for {Location} in {Year}",
            prayerTimeEntities.Count, location.Name, currentYear);

        return new Message
        {
            MessageType = MessageType.Success,
            Value = $"Successfully generated {prayerTimeEntities.Count} prayer time records for {location.Name} ({currentYear})."
        };
    }

    /// <inheritdoc />
    public async Task<(TodayPrayerTimesDto? Model, Message Message)> GetTodayPrayerTimesAsync(
        string locationCode,
        CancellationToken ct = default)
    {
        var location = await _unitOfWork.Locations
            .Query()
            .FirstOrDefaultAsync(l => l.Code == locationCode && l.IS_ACTIVE, ct);

        if (location == null)
        {
            return (null, MessageFactory.CreateErrorMessage(
                string.Format(Constants.ApplicationMessages.NOT_FOUND, "Location")));
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var prayerTime = await _unitOfWork.PrayerTimes
            .Query()
            .FirstOrDefaultAsync(
                pt => pt.LocationId == location.Id && pt.PrayerDate == today,
                ct);

        if (prayerTime == null)
        {
            return (null, MessageFactory.CreateErrorMessage(
                "Prayer times not found for today. Please generate yearly prayer times first."));
        }

        static string FormatTime(TimeOnly time) =>
            time.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

        var dto = new TodayPrayerTimesDto
        {
            LocationCode = location.Code,
            LocationName = location.Name,
            PrayerDate = prayerTime.PrayerDate,
            Fajr = FormatTime(prayerTime.Fajr),
            Sunrise = FormatTime(prayerTime.Sunrise),
            Dhuhr = FormatTime(prayerTime.Dhuhr),
            Asr = FormatTime(prayerTime.Asr),
            Maghrib = FormatTime(prayerTime.Maghrib),
            Isha = FormatTime(prayerTime.Isha)
        };

        return (dto, new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });
    }

    /// <inheritdoc />
    public async Task<(List<PrayerDayTimesDto> Model, Message Message)> GetUpcomingPrayerTimesAsync(
        string locationCode,
        int days = 7,
        CancellationToken ct = default)
    {
        var location = await _unitOfWork.Locations
            .Query()
            .FirstOrDefaultAsync(l => l.Code == locationCode && l.IS_ACTIVE, ct);

        if (location == null)
        {
            return ([], MessageFactory.CreateErrorMessage(
                string.Format(Constants.ApplicationMessages.NOT_FOUND, "Location")));
        }

        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        var endDate = startDate.AddDays(Math.Max(1, days) - 1);

        var rows = await _unitOfWork.PrayerTimes
            .Query()
            .Where(pt => pt.LocationId == location.Id
                         && pt.PrayerDate >= startDate
                         && pt.PrayerDate <= endDate)
            .OrderBy(pt => pt.PrayerDate)
            .ToListAsync(ct);

        if (rows.Count == 0)
        {
            return ([], MessageFactory.CreateErrorMessage(
                "Prayer times not found. Please generate yearly prayer times first."));
        }

        static string FormatTime(TimeOnly time) =>
            time.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

        var list = rows.Select(pt => new PrayerDayTimesDto
        {
            LocationCode = location.Code,
            PrayerDate = pt.PrayerDate,
            Fajr = FormatTime(pt.Fajr),
            Sunrise = FormatTime(pt.Sunrise),
            Dhuhr = FormatTime(pt.Dhuhr),
            Asr = FormatTime(pt.Asr),
            Maghrib = FormatTime(pt.Maghrib),
            Isha = FormatTime(pt.Isha)
        }).ToList();

        return (list, new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });
    }
}
