using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Notification.Contracts;
using NamazTimeApp.Notification.Service;

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

    public static IServiceCollection AddNotificationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<FirebaseSettings>(
            configuration.GetSection(FirebaseSettings.SectionName));

        services.AddHttpClient();
        services.AddScoped<IFcmNotificationService, FcmNotificationService>();
        services.AddScoped<IAdhanNotificationDispatcher, AdhanNotificationDispatcher>();
        services.AddHostedService<AdhanNotificationHostedService>();

        return services;
    }
}
