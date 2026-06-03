namespace NamazTimeApp.Notification.Contracts;

public class FirebaseSettings
{
    public const string SectionName = "Firebase";

    public string ProjectId { get; set; } = string.Empty;

    /// <summary>Absolute path to Firebase service account JSON file.</summary>
    public string ServiceAccountJsonPath { get; set; } = string.Empty;

    public bool Enabled { get; set; }
}
