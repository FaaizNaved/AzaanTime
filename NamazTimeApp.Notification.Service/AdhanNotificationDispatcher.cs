using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NamazTimeApp.Core;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Notification;
using NamazTimeApp.Notification.Contracts;
using NamazTimeApp.Transaction.Contracts;

namespace NamazTimeApp.Notification.Service;

public class AdhanNotificationDispatcher : IAdhanNotificationDispatcher
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static readonly (string Code, string Name, Func<PrayerTimeRow, TimeOnly> TimeSelector, string Sound)[] PrayerSlots =
    [
        ("FAJR", "Fajr", pt => pt.Fajr, "azan_fajr"),
        ("SUNRISE", "Sunrise", pt => pt.Sunrise, "azan_default"),
        ("DHUHR", "Dhuhr", pt => pt.Dhuhr, "azan_default"),
        ("ASR", "Asr", pt => pt.Asr, "azan_default"),
        ("MAGHRIB", "Maghrib", pt => pt.Maghrib, "azan_default"),
        ("ISHA", "Isha", pt => pt.Isha, "azan_default")
    ];

    private readonly IAppUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IFcmNotificationService _fcmService;
    private readonly ILogger<AdhanNotificationDispatcher> _logger;

    public AdhanNotificationDispatcher(
        IAppUnitOfWork<AppDbContext> unitOfWork,
        IFcmNotificationService fcmService,
        ILogger<AdhanNotificationDispatcher> logger)
    {
        _unitOfWork = unitOfWork;
        _fcmService = fcmService;
        _logger = logger;
    }

    public async Task DispatchDueNotificationsAsync(CancellationToken ct = default)
    {
        var devices = await _unitOfWork.DeviceRegistrations
            .Query()
            .Include(d => d.Location)
            .Where(d => d.IS_ACTIVE
                        && d.NotificationEnabled
                        && d.CurrentFcmToken != null
                        && d.Location != null)
            .ToListAsync(ct);

        if (devices.Count == 0)
        {
            return;
        }

        var prayerTypes = await _unitOfWork.PrayerTypes
            .Query()
            .Where(pt => pt.IS_ACTIVE)
            .ToDictionaryAsync(pt => pt.Code, pt => pt.Id, ct);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var device in devices)
        {
            var location = device.Location!;
            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(location.TimeZone);
            }
            catch
            {
                _logger.LogWarning("Invalid timezone {Tz} for device {Device}", location.TimeZone, device.DeviceUniqueId);
                continue;
            }

            var localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            var localToday = DateOnly.FromDateTime(localNow);

            var prayerTime = await _unitOfWork.PrayerTimes
                .Query()
                .FirstOrDefaultAsync(
                    pt => pt.LocationId == location.Id && pt.PrayerDate == localToday,
                    ct);

            if (prayerTime == null)
            {
                continue;
            }

            var prefs = DeserializePreferences(device.PrayerPreferencesJson);
            var row = new PrayerTimeRow(
                prayerTime.Fajr,
                prayerTime.Sunrise,
                prayerTime.Dhuhr,
                prayerTime.Asr,
                prayerTime.Maghrib,
                prayerTime.Isha);

            foreach (var slot in PrayerSlots)
            {
                if (!IsPrayerEnabled(prefs, slot.Code))
                {
                    continue;
                }

                if (!prayerTypes.TryGetValue(slot.Code, out var prayerTypeId))
                {
                    continue;
                }

                var scheduledLocal = localToday.ToDateTime(slot.TimeSelector(row));
                if (Math.Abs((localNow - scheduledLocal).TotalMinutes) > 1)
                {
                    continue;
                }

                var alreadySent = await _unitOfWork.NotificationLogs
                    .Query()
                    .AnyAsync(
                        n => n.DeviceRegistrationId == device.Id
                             && n.PrayerTypeId == prayerTypeId
                             && n.PrayerDate == localToday
                             && n.Status == "Sent",
                        ct);

                if (alreadySent)
                {
                    continue;
                }

                var volume = GetPrayerVolume(prefs, slot.Code);
                var sound = slot.Sound;
                var sent = await _fcmService.SendAdhanNotificationAsync(
                    device.CurrentFcmToken!,
                    slot.Code,
                    slot.Name,
                    $"Adhan — {slot.Name}",
                    $"It is time for {slot.Name} prayer.",
                    sound,
                    volume,
                    ct);

                var log = new NotificationLog
                {
                    Id = Guid.NewGuid(),
                    DeviceRegistrationId = device.Id,
                    PrayerTypeId = prayerTypeId,
                    PrayerDate = localToday,
                    NotificationTitle = $"Adhan — {slot.Name}",
                    NotificationBody = $"It is time for {slot.Name} prayer.",
                    SentOn = DateTime.UtcNow,
                    Status = sent ? "Sent" : "Failed",
                    FailureReason = sent ? null : "FCM delivery failed",
                    CREATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID,
                    IS_ACTIVE = true,
                    RECORD_SOURCE_NAME = Constants.Common.RECORD_SOURCE
                };

                await _unitOfWork.NotificationLogs.AddAsync(log, ct);
                await _unitOfWork.SaveChangesAsync(ct);
            }
        }
    }

    private static DevicePrayerPreferencesDto? DeserializePreferences(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        return JsonSerializer.Deserialize<DevicePrayerPreferencesDto>(json, JsonOptions);
    }

    private static bool IsPrayerEnabled(DevicePrayerPreferencesDto? prefs, string code)
    {
        if (prefs == null)
        {
            return code != "SUNRISE";
        }

        var p = GetPreference(prefs, code);
        return p?.Enabled ?? code != "SUNRISE";
    }

    private static int GetPrayerVolume(DevicePrayerPreferencesDto? prefs, string code)
    {
        var p = prefs == null ? null : GetPreference(prefs, code);
        return p?.Volume ?? 70;
    }

    private static PrayerPreferenceDto? GetPreference(DevicePrayerPreferencesDto prefs, string code) =>
        code switch
        {
            "FAJR" => prefs.Fajr,
            "SUNRISE" => prefs.Sunrise,
            "DHUHR" => prefs.Dhuhr,
            "ASR" => prefs.Asr,
            "MAGHRIB" => prefs.Maghrib,
            "ISHA" => prefs.Isha,
            _ => null
        };

    private sealed record PrayerTimeRow(
        TimeOnly Fajr,
        TimeOnly Sunrise,
        TimeOnly Dhuhr,
        TimeOnly Asr,
        TimeOnly Maghrib,
        TimeOnly Isha);
}
