using NamazTimeApp.Core.Data;
using NamazTimeApp.Transaction;
using NamazTimeApp.Transaction.Repositories;

namespace NamazTimeApp.Infrastructure.Data.TransactionRepositories;

public class PrayerTimeRepository : GenericRepository<Guid, PrayerTime>, IPrayerTimeRepository
{
    public PrayerTimeRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
