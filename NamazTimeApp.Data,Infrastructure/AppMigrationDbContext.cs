using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NamazTimeApp.Data.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class AppMigrationDbContext : AppDbContext
    {
        private readonly IConfiguration _configuration;

        public AppMigrationDbContext() { }

        public AppMigrationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppMigrationDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql
            (
                _configuration.GetConnectionString(Constants.ConfigurationKeys.CONNECTION_STRING)
            );
        }
    }
}
