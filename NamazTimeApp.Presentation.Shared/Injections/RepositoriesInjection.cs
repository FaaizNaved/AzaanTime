using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Infrastructure.Data.MasterRepositories;
using NamazTimeApp.Infrastructure.Data.NotificationRepositories;
using NamazTimeApp.Infrastructure.Data.TransactionRepositories;
using NamazTimeApp.Master.Repositories;
using NamazTimeApp.Notification.Repositories;
using NamazTimeApp.Transaction.Repositories;

namespace NamazTimeApp.Presentation.Shared.Injections;

public static class RepositoriesInjection
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppUnitOfWork<AppDbContext>, AppUnitOfWork<AppDbContext>>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IPrayerTypeRepository, PrayerTypeRepository>();
        services.AddScoped<IAppSettingRepository, AppSettingRepository>();
        services.AddScoped<IPrayerTimeRepository, PrayerTimeRepository>();
        services.AddScoped<IDeviceRegistrationRepository, DeviceRegistrationRepository>();
        services.AddScoped<INotificationLogRepository, NotificationLogRepository>();

        return services;
    }
}
