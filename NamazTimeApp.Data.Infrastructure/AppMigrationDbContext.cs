using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NamazTimeApp.Core;
using NamazTimeApp.Infrastructure.Data;

namespace NamazTimeApp.Data.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AppMigrationDbContext : AppDbContext
    {
        private readonly IConfiguration? _configuration;

        public AppMigrationDbContext()
        {
        }

        public AppMigrationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppMigrationDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured || _configuration is null)
            {
                return;
            }

            optionsBuilder.UseNpgsql(
                _configuration.GetConnectionString(Constants.ConfigurationKeys.CONNECTION_STRING));
        }
    }
}
