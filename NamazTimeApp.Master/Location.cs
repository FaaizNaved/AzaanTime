using NamazTimeApp.Core;

namespace NamazTimeApp.Master;

public class Location : EntityBase
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string StateName { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string TimeZone { get; set; } = string.Empty;
}
