using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public DbContext DbContext => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
