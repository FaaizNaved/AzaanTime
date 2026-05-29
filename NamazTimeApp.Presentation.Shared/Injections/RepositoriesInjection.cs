using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Infrastructure.Data;
using NamazTimeApp.Infrastructure.Data.Interface;

namespace NamazTimeApp.Presentation.Shared.Injections;

public static class RepositoriesInjection
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppUnitOfWork<AppDbContext>, AppUnitOfWork<AppDbContext>>();

        return services;
    }
}
