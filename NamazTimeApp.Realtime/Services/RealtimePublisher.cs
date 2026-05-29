using Microsoft.AspNetCore.SignalR;
using NamazTimeApp.Realtime.Hubs;
using NamazTimeApp.Realtime.Interfaces;

namespace NamazTimeApp.Realtime.Services;

public class RealtimePublisher : IRealtimePublisher
{
    private readonly IHubContext<NamazTimeHub> _hubContext;

    public RealtimePublisher(IHubContext<NamazTimeHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task PublishAsync(
        string eventName,
        object payload,
        CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync(eventName, payload, cancellationToken);
    }
}
