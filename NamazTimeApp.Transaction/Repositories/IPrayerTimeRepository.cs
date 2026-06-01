using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Transaction.Repositories;

public interface IPrayerTimeRepository : IGenericRepository<Guid, PrayerTime>
{
}
