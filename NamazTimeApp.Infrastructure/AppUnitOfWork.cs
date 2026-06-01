using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core.Data;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Infrastructure.Data.MasterRepositories;
using NamazTimeApp.Infrastructure.Data.NotificationRepositories;
using NamazTimeApp.Infrastructure.Data.TransactionRepositories;
using NamazTimeApp.Master.Repositories;
using NamazTimeApp.Notification.Repositories;
using NamazTimeApp.Transaction.Repositories;

namespace NamazTimeApp.Infrastructure.Data;

public class AppUnitOfWork<T> : UnitOfWork<T>, IAppUnitOfWork<T> where T : DbContext
{
    private readonly AppDbContext _appDbContext;
    private ILocationRepository? _locations;
    private IPrayerTypeRepository? _prayerTypes;
    private IAppSettingRepository? _appSettings;
    private IPrayerTimeRepository? _prayerTimes;
    private IDeviceRegistrationRepository? _deviceRegistrations;
    private INotificationLogRepository? _notificationLogs;

    public AppUnitOfWork(AppDbContext dbContext)
        : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public ILocationRepository Locations => _locations ??= new LocationRepository(_appDbContext);

    public IPrayerTypeRepository PrayerTypes => _prayerTypes ??= new PrayerTypeRepository(_appDbContext);

    public IAppSettingRepository AppSettings => _appSettings ??= new AppSettingRepository(_appDbContext);

    public IPrayerTimeRepository PrayerTimes => _prayerTimes ??= new PrayerTimeRepository(_appDbContext);

    public IDeviceRegistrationRepository DeviceRegistrations => _deviceRegistrations ??= new DeviceRegistrationRepository(_appDbContext);

    public INotificationLogRepository NotificationLogs => _notificationLogs ??= new NotificationLogRepository(_appDbContext);
}
