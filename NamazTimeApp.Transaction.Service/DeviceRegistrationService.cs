using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Transaction.Contracts;

namespace NamazTimeApp.Transaction.Service;

public class DeviceRegistrationService : IDeviceRegistrationService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IAppUnitOfWork<AppDbContext> _unitOfWork;

    public DeviceRegistrationService(IAppUnitOfWork<AppDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<(DeviceRegistrationDto? Model, Message Message)> RegisterOrUpdateAsync(
        RegisterDeviceRequestDto request,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(request.DeviceUniqueId))
        {
            return (null, MessageFactory.CreateErrorMessage("DeviceUniqueId is required."));
        }

        var location = await _unitOfWork.Locations
            .Query()
            .FirstOrDefaultAsync(l => l.Code == request.LocationCode && l.IS_ACTIVE, ct);

        if (location == null)
        {
            return (null, MessageFactory.CreateErrorMessage(
                string.Format(Constants.ApplicationMessages.NOT_FOUND, "Location")));
        }

        var existing = await _unitOfWork.DeviceRegistrations
            .Query()
            .FirstOrDefaultAsync(d => d.DeviceUniqueId == request.DeviceUniqueId, ct);

        var prefsJson = request.PrayerPreferences != null
            ? JsonSerializer.Serialize(request.PrayerPreferences, JsonOptions)
            : null;

        if (existing == null)
        {
            existing = new DeviceRegistration
            {
                Id = Guid.NewGuid(),
                DeviceUniqueId = request.DeviceUniqueId,
                Platform = request.Platform,
                LocationId = location.Id,
                CurrentFcmToken = request.FcmToken,
                AppVersion = request.AppVersion,
                NotificationEnabled = request.NotificationEnabled,
                PrayerPreferencesJson = prefsJson,
                LastSeenOn = DateTime.UtcNow,
                CREATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID,
                IS_ACTIVE = true,
                RECORD_SOURCE_NAME = Constants.Common.RECORD_SOURCE
            };

            await _unitOfWork.DeviceRegistrations.AddAsync(existing, ct);
        }
        else
        {
            existing.Platform = request.Platform;
            existing.LocationId = location.Id;
            existing.CurrentFcmToken = request.FcmToken ?? existing.CurrentFcmToken;
            existing.AppVersion = request.AppVersion;
            existing.NotificationEnabled = request.NotificationEnabled;
            if (prefsJson != null)
            {
                existing.PrayerPreferencesJson = prefsJson;
            }

            existing.LastSeenOn = DateTime.UtcNow;
            existing.UPDATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID;
            existing.UPDATED_DATE = DateTime.UtcNow;
            _unitOfWork.DeviceRegistrations.Update(existing);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return (MapToDto(existing, location.Code), new Message
        {
            MessageType = MessageType.Success,
            Value = string.Format(Constants.ApplicationMessages.SUCCESS_UPDATED, "Device")
        });
    }

    /// <inheritdoc />
    public async Task<(DeviceRegistrationDto? Model, Message Message)> UpdatePreferencesAsync(
        string deviceUniqueId,
        DevicePrayerPreferencesDto preferences,
        bool? notificationEnabled,
        CancellationToken ct = default)
    {
        var device = await _unitOfWork.DeviceRegistrations
            .Query()
            .Include(d => d.Location)
            .FirstOrDefaultAsync(d => d.DeviceUniqueId == deviceUniqueId, ct);

        if (device == null)
        {
            return (null, MessageFactory.CreateErrorMessage(
                string.Format(Constants.ApplicationMessages.NOT_FOUND, "Device")));
        }

        device.PrayerPreferencesJson = JsonSerializer.Serialize(preferences, JsonOptions);
        if (notificationEnabled.HasValue)
        {
            device.NotificationEnabled = notificationEnabled.Value;
        }

        device.LastSeenOn = DateTime.UtcNow;
        device.UPDATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID;
        device.UPDATED_DATE = DateTime.UtcNow;
        _unitOfWork.DeviceRegistrations.Update(device);
        await _unitOfWork.SaveChangesAsync(ct);

        return (MapToDto(device, device.Location?.Code ?? string.Empty), new Message
        {
            MessageType = MessageType.Success,
            Value = string.Format(Constants.ApplicationMessages.SUCCESS_UPDATED, "Device")
        });
    }

    private static DeviceRegistrationDto MapToDto(DeviceRegistration device, string locationCode)
    {
        DevicePrayerPreferencesDto? prefs = null;
        if (!string.IsNullOrWhiteSpace(device.PrayerPreferencesJson))
        {
            prefs = JsonSerializer.Deserialize<DevicePrayerPreferencesDto>(
                device.PrayerPreferencesJson, JsonOptions);
        }

        return new DeviceRegistrationDto
        {
            Id = device.Id,
            DeviceUniqueId = device.DeviceUniqueId,
            LocationCode = locationCode,
            NotificationEnabled = device.NotificationEnabled,
            PrayerPreferences = prefs
        };
    }
}
