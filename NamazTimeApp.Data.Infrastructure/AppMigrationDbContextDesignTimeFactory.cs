using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NamazTimeApp.Infrastructure.Data;

namespace NamazTimeApp.Data.Infrastructure;

public class AppMigrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppMigrationDbContext>
{
    public AppMigrationDbContext CreateDbContext(string[] args)
    {
        var connectionString = DesignTimeConfiguration.GetConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsHistoryTable("__EFMigrationsHistory", "NAMAZTIMEAPP_MASTER");
        });

        return new AppMigrationDbContext(optionsBuilder.Options);
    }
}
