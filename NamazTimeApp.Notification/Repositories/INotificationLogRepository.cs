using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Notification.Repositories;

public interface INotificationLogRepository : IGenericRepository<Guid, NotificationLog>
{
}
