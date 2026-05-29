using Microsoft.Extensions.DependencyInjection;
using NamazTimeApp.Realtime.Interfaces;
using NamazTimeApp.Realtime.Services;

namespace NamazTimeApp.Realtime.Extensions;

public static class SignalRInjection
{
    public static IServiceCollection AddRealtimeServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<IRealtimePublisher, RealtimePublisher>();

        return services;
    }
}
