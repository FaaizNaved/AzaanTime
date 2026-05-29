using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core.Data;
using NamazTimeApp.Infrastructure.Data.Interface;

namespace NamazTimeApp.Infrastructure.Data;

public class AppUnitOfWork<T> : UnitOfWork<T>, IAppUnitOfWork<T> where T : DbContext
{
    public AppUnitOfWork(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
