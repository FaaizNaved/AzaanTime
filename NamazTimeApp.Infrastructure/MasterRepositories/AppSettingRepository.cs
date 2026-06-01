using NamazTimeApp.Core.Data;
using NamazTimeApp.Master;
using NamazTimeApp.Master.Repositories;

namespace NamazTimeApp.Infrastructure.Data.MasterRepositories;

public class AppSettingRepository : GenericRepository<Guid, AppSetting>, IAppSettingRepository
{
    public AppSettingRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
