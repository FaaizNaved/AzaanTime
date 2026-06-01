using NamazTimeApp.Core.Data;
using NamazTimeApp.Master;
using NamazTimeApp.Master.Repositories;

namespace NamazTimeApp.Infrastructure.Data.MasterRepositories;

public class PrayerTypeRepository : GenericRepository<Guid, PrayerType>, IPrayerTypeRepository
{
    public PrayerTypeRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
