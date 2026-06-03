using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NamazTimeApp.Core;
using NamazTimeApp.Core.Infrastructure;
using NamazTimeApp.Master.Contracts;

namespace NamazTimeApp.Application.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/locations")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Returns distinct country names from the Location table.
    /// </summary>
    [HttpGet("countries")]
    public async Task<IActionResult> GetDistinctCountries(CancellationToken ct)
    {
        var countries = await _locationService.GetDistinctCountriesAsync(ct);

        var response = new Response<List<string>>
        {
            Model = countries,
            TotalRows = countries.Count
        };

        response.Messages.Add(new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });

        return Ok(response);
    }

    /// <summary>
    /// Returns distinct state names from the Location table.
    /// </summary>
    [HttpGet("states")]
    public async Task<IActionResult> GetDistinctStates(CancellationToken ct)
    {
        var states = await _locationService.GetDistinctStatesAsync(ct);

        var response = new Response<List<string>>
        {
            Model = states,
            TotalRows = states.Count
        };

        response.Messages.Add(new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });

        return Ok(response);
    }

    /// <summary>
    /// Returns distinct city names from the Location table.
    /// </summary>
    [HttpGet("cities")]
    public async Task<IActionResult> GetDistinctCities(CancellationToken ct)
    {
        var cities = await _locationService.GetDistinctCitiesAsync(ct);

        var response = new Response<List<string>>
        {
            Model = cities,
            TotalRows = cities.Count
        };

        response.Messages.Add(new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });

        return Ok(response);
    }

    /// <summary>
    /// Resolves a location by country, state, and city name.
    /// </summary>
    [HttpGet("resolve")]
    public async Task<IActionResult> ResolveLocation(
        [FromQuery] string country,
        [FromQuery] string state,
        [FromQuery] string city,
        CancellationToken ct)
    {
        var location = await _locationService.ResolveLocationAsync(country, state, city, ct);

        var response = new Response<LocationDto>
        {
            Model = location!,
            TotalRows = location != null ? 1 : 0
        };

        if (location == null)
        {
            response.Messages.Add(new Message
            {
                MessageType = MessageType.Error,
                Value = string.Format(Constants.ApplicationMessages.NOT_FOUND, "Location")
            });
            return NotFound(response);
        }

        response.Messages.Add(new Message
        {
            MessageType = MessageType.Success,
            Value = Constants.ApplicationMessages.DATA_FETCHED_SUCCESSFULLY
        });

        return Ok(response);
    }
}
