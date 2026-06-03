using NamazTimeApp.Core;

namespace NamazTimeApp.Transaction.Contracts;

public interface IDeviceRegistrationService
{
    Task<(DeviceRegistrationDto? Model, Message Message)> RegisterOrUpdateAsync(
        RegisterDeviceRequestDto request,
        CancellationToken ct = default);

    Task<(DeviceRegistrationDto? Model, Message Message)> UpdatePreferencesAsync(
        string deviceUniqueId,
        DevicePrayerPreferencesDto preferences,
        bool? notificationEnabled,
        CancellationToken ct = default);
}
