using NamazTimeApp.Core;

namespace NamazTimeApp.Master;

public class PrayerType : EntityBase
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}
