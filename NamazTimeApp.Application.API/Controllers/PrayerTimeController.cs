using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NamazTimeApp.Core;
using NamazTimeApp.Core.Infrastructure;
using NamazTimeApp.Transaction.Contracts;

namespace NamazTimeApp.Application.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/prayer-times")]
public class PrayerTimeController : ControllerBase
{
    private readonly IPrayerTimeService _prayerTimeService;

    public PrayerTimeController(IPrayerTimeService prayerTimeService)
    {
        _prayerTimeService = prayerTimeService;
    }

    /// <summary>
    /// Generates prayer times for the entire current year for the given location.
    /// </summary>
    /// <param name="locationCode">The location code (e.g., KANPUR).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Generation result with count of days generated.</returns>
    [HttpPost("generate/{locationCode}")]
    public async Task<IActionResult> GenerateYearlyPrayerTimes(
        string locationCode,
        CancellationToken ct)
    {
        var result = await _prayerTimeService.GenerateYearlyPrayerTimesAsync(locationCode, ct);

        var response = new Response<object>
        {
            Model = null!
        };

        response.Messages.Add(result);

        return result.MessageType == MessageType.Success
            ? Ok(response)
            : BadRequest(response);
    }

    /// <summary>
    /// Returns today's prayer times for the given location code.
    /// </summary>
    [HttpGet("today/{locationCode}")]
    public async Task<IActionResult> GetTodayPrayerTimes(
        string locationCode,
        CancellationToken ct)
    {
        var (model, message) = await _prayerTimeService.GetTodayPrayerTimesAsync(locationCode, ct);

        var response = new Response<TodayPrayerTimesDto>
        {
            Model = model!,
            TotalRows = model != null ? 1 : 0
        };
        response.Messages.Add(message);

        return message.MessageType == MessageType.Success
            ? Ok(response)
            : BadRequest(response);
    }
}
