using NamazTimeApp.Core.Data;
using NamazTimeApp.Master;
using NamazTimeApp.Master.Repositories;

namespace NamazTimeApp.Infrastructure.Data.MasterRepositories;

public class LocationRepository : GenericRepository<Guid, Location>, ILocationRepository
{
    public LocationRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
