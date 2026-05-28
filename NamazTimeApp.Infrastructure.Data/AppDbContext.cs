using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NamazTimeApp.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public partial class AppDbContext :
     IdentityDbContext<
         EN_MSTR_User,
         EN_MSTR_Role,
         int,
         IdentityUserClaim<int>,
         EN_MSTR_UserRole,
         IdentityUserLogin<int>,
         IdentityRoleClaim<int>,
         IdentityUserToken<int>>
    {
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext() : base() { }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ILogger<AppDbContext> logger = null)
            : base(options)
        {
            _logger = logger;
        }

        // Sync SaveChanges with retry
        public override int SaveChanges()
        {
            int retryCount = 0;
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
                        throw;

                    foreach (var entry in ex.Entries)
                    {
                        entry.Reload();
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger?.LogError(ex, "Database update failed (Sync)");
                    throw; // preserve original exception
                }
            }
        }

        // Async SaveChanges with retry
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            int retryCount = 0;
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
                        throw;

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync(cancellationToken);
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger?.LogError(ex, "Database update failed (Async)");
                    throw; // let middleware handle it
                }
            }
        }

        // Generic DbSet accessor
        public DbSet<TEntity> GetEntitySet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        // Expose DbContext if needed (optional)
        public DbContext DbContext => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyMasterConfigurations();
            modelBuilder.ApplyTransactionConfigurations();
            modelBuilder.ApplyNotificationConfigurations();
        }
    }
}
