using NamazTimeApp.Core.Data;
using NamazTimeApp.Transaction;
using NamazTimeApp.Transaction.Repositories;

namespace NamazTimeApp.Infrastructure.Data.TransactionRepositories;

public class DeviceRegistrationRepository : GenericRepository<Guid, DeviceRegistration>, IDeviceRegistrationRepository
{
    public DeviceRegistrationRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
