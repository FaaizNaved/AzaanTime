using NamazTimeApp.Core;

namespace NamazTimeApp.Master;

public class AppSetting : EntityBase
{
    public Guid Id { get; set; }

    public string SettingKey { get; set; } = string.Empty;

    public string SettingValue { get; set; } = string.Empty;

    public string? Description { get; set; }
}
