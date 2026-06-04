using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NamazTimeApp.Notification.Contracts;

namespace NamazTimeApp.Notification.Service;

public class FcmNotificationService : IFcmNotificationService
{
    private readonly FirebaseSettings _settings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FcmNotificationService> _logger;

    public FcmNotificationService(
        IOptions<FirebaseSettings> settings,
        IHttpClientFactory httpClientFactory,
        ILogger<FcmNotificationService> logger)
    {
        _settings = settings.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<bool> SendAdhanNotificationAsync(
        string fcmToken,
        string prayerCode,
        string prayerName,
        string title,
        string body,
        string soundFile,
        int volume,
        CancellationToken ct = default)
    {
        if (!_settings.Enabled || string.IsNullOrWhiteSpace(_settings.ProjectId))
        {
            _logger.LogWarning("FCM is disabled or ProjectId is missing.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(fcmToken))
        {
            return false;
        }

        var isFajr = prayerCode.Equals("FAJR", StringComparison.OrdinalIgnoreCase);
        var channelId = isFajr ? "adhan_alarm_fajr" : "adhan_alarm_default";
        var soundResource = isFajr ? "azan_fajr" : "azan_default";

        try
        {
            var accessToken = await GetAccessTokenAsync(ct);
            var payload = new
            {
                message = new
                {
                    token = fcmToken,
                    notification = new { title, body },
                    data = new Dictionary<string, string>
                    {
                        ["type"] = "adhan",
                        ["prayerCode"] = prayerCode,
                        ["prayerName"] = prayerName,
                        ["prayer"] = prayerCode.ToLowerInvariant(),
                        ["sound"] = soundResource,
                        ["volume"] = volume.ToString()
                    },
                    android = new
                    {
                        priority = "high",
                        notification = new
                        {
                            channel_id = channelId,
                            sound = soundResource,
                            notification_priority = "PRIORITY_MAX",
                            visibility = "PUBLIC",
                            default_vibrate_timings = true,
                            sticky = false
                        }
                    },
                    apns = new
                    {
                        payload = new
                        {
                            aps = new
                            {
                                sound = $"{soundResource}.mp3",
                                content_available = true,
                                interruption_level = "time-sensitive"
                            }
                        }
                    }
                }
            };

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var url =
                $"https://fcm.googleapis.com/v1/projects/{_settings.ProjectId}/messages:send";

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content, ct);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(ct);
                _logger.LogError("FCM send failed: {Status} {Error}", response.StatusCode, error);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "FCM send exception for prayer {Prayer}", prayerCode);
            return false;
        }
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(_settings.ServiceAccountJsonPath)
            || !File.Exists(_settings.ServiceAccountJsonPath))
        {
            throw new InvalidOperationException(
                "Firebase ServiceAccountJsonPath is not configured or file is missing.");
        }

        using var stream = File.OpenRead(_settings.ServiceAccountJsonPath);
        var credential = GoogleCredential
            .FromStream(stream)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync(cancellationToken: ct);
    }
}
