using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Master.Contracts;
using NamazTimeApp.Master.Service;
using NamazTimeApp.Transaction.Contracts;
using NamazTimeApp.Transaction.Service;

namespace NamazTimeApp.Presentation.Shared.Injections;

public static class ServicesInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IPrayerTimeService, PrayerTimeService>();
        services.AddScoped<IDeviceRegistrationService, DeviceRegistrationService>();

        return services;
    }
}
