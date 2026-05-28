using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using NamazTimeApp.Core;
using NamazTimeApp.Infrastructure;
using NamazTimeApp.Infrastructure.Data.Interface;
using NamazTimeApp.Master;

namespace NamazTimeApp.Data.Infrastructure.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp, ILogger logger)
        {
            using var scope = webApp.Services.CreateScope();

            using var appContext = scope.ServiceProvider.GetRequiredService<AppMigrationDbContext>();
            using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAppUnitOfWork<AppDbContext>>();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EN_MSTR_User>>();
            try
            {
                appContext.Database.Migrate();

                DataSeeder.Seed(unitOfWork, userManager);
            }
            catch (Exception ex)
            {
                logger.Error(Constants.ApplicationMessages.ERROR_DB_MIGRATION,
                    ex.Message,
                    ex.StackTrace);
            }
            return webApp;
        }
    }
}
