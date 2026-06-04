using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NamazTimeApp.Presentation.Shared.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddAppOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    public static IServiceCollection AddAwsServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    /// <summary>
    /// Adhan is delivered via local scheduled notifications on the device (no FCM).
    /// </summary>
    public static IServiceCollection AddNotificationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
