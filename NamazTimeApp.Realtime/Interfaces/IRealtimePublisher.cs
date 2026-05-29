namespace NamazTimeApp.Realtime.Interfaces;

public interface IRealtimePublisher
{
    Task PublishAsync(string eventName, object payload, CancellationToken cancellationToken = default);
}
