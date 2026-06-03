using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NamazTimeApp.Core;
using NamazTimeApp.Core.Infrastructure;
using NamazTimeApp.Transaction.Contracts;

namespace NamazTimeApp.Application.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/devices")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceRegistrationService _deviceService;

    public DeviceController(IDeviceRegistrationService deviceService)
    {
        _deviceService = deviceService;
    }

    /// <summary>
    /// Registers or updates a device with FCM token, location, and notification preferences.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterDeviceRequestDto request,
        CancellationToken ct)
    {
        var (model, message) = await _deviceService.RegisterOrUpdateAsync(request, ct);

        var response = new Response<DeviceRegistrationDto?>
        {
            Model = model,
            TotalRows = model != null ? 1 : 0
        };
        response.Messages.Add(message);

        return message.MessageType == MessageType.Success
            ? Ok(response)
            : BadRequest(response);
    }

    /// <summary>
    /// Updates prayer notification preferences for a registered device.
    /// </summary>
    [HttpPut("{deviceUniqueId}/preferences")]
    public async Task<IActionResult> UpdatePreferences(
        string deviceUniqueId,
        [FromBody] DevicePrayerPreferencesDto preferences,
        [FromQuery] bool? notificationEnabled,
        CancellationToken ct)
    {
        var (model, message) = await _deviceService.UpdatePreferencesAsync(
            deviceUniqueId,
            preferences,
            notificationEnabled,
            ct);

        var response = new Response<DeviceRegistrationDto?>
        {
            Model = model,
            TotalRows = model != null ? 1 : 0
        };
        response.Messages.Add(message);

        return message.MessageType == MessageType.Success
            ? Ok(response)
            : BadRequest(response);
    }
}
