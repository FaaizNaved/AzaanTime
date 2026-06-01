using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NamazTimeApp.Master;
using NamazTimeApp.Notification;
using NamazTimeApp.Transaction;

namespace NamazTimeApp.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public partial class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext>? _logger;

        public AppDbContext()
        {
        }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ILogger<AppDbContext>? logger = null)
            : base(options)
        {
            _logger = logger;
        }

        public override int SaveChanges()
        {
            var retryCount = 0;
            const int maxRetries = 3;

            while (true)
            {
                try
                {
                    return base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    retryCount++;

                    if (retryCount >= maxRetries)
                    {
                        throw;
                    }

                    foreach (var entry in ex.Entries)
                    {
                        entry.Reload();
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger?.LogError(ex, "Database update failed (Sync)");
                    throw;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var retryCount = 0;
            const int maxRetries = 3;

            while (true)
            {
                try
                {
                    return await base.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    retryCount++;

                    if (retryCount >= maxRetries)
                    {
                        throw;
                    }

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync(cancellationToken);
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger?.LogError(ex, "Database update failed (Async)");
                    throw;
                }
            }
        }

        public DbSet<TEntity> GetEntitySet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<PrayerType> PrayerTypes => Set<PrayerType>();

        public DbSet<AppSetting> AppSettings => Set<AppSetting>();

        public DbSet<PrayerTime> PrayerTimes => Set<PrayerTime>();

        public DbSet<DeviceRegistration> DeviceRegistrations => Set<DeviceRegistration>();

        public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();

        public DbContext DbContext => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NamazTimeApp.Master.Data.Configurations.LocationConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NamazTimeApp.Transaction.Data.Configurations.PrayerTimeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NamazTimeApp.Notification.Data.Configurations.NotificationLogConfiguration).Assembly);
        }
    }
}
