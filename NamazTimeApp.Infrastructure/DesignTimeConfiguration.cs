using Microsoft.Extensions.Configuration;
using NamazTimeApp.Core;

namespace NamazTimeApp.Infrastructure.Data;

public static class DesignTimeConfiguration
{
    public static string GetConnectionString()
    {
        var basePath = Directory.GetCurrentDirectory();
        var apiProjectPath = Path.Combine(basePath, "NamazTimeApp.Application.API");

        if (!Directory.Exists(apiProjectPath))
        {
            apiProjectPath = Path.Combine(basePath, "..", "NamazTimeApp.Application.API");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(apiProjectPath))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        return configuration.GetConnectionString(Constants.ConfigurationKeys.CONNECTION_STRING)
            ?? throw new InvalidOperationException("Connection string is not configured.");
    }
}
