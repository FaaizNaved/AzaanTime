using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Core;
using Serilog;

namespace NamazTimeApp.Data.Infrastructure.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp, ILogger logger)
        {
            using var scope = webApp.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<AppMigrationDbContext>();

            try
            {
                appContext.Database.Migrate();
                DataSeeder.Seed();
            }
            catch (Exception ex)
            {
                logger.Error(
                    Constants.ApplicationMessages.ERROR_DB_MIGRATION,
                    ex.Message,
                    ex.StackTrace);
            }

            return webApp;
        }
    }
}
