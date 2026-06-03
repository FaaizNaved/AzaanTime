namespace NamazTimeApp.Master.Contracts;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string StateName { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
}
