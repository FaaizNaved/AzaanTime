using NamazTimeApp.Core.Data;
using NamazTimeApp.Notification;
using NamazTimeApp.Notification.Repositories;

namespace NamazTimeApp.Infrastructure.Data.NotificationRepositories;

public class NotificationLogRepository : GenericRepository<Guid, NotificationLog>, INotificationLogRepository
{
    public NotificationLogRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
