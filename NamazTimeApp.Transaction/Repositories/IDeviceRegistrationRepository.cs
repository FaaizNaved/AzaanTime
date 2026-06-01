using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Transaction.Repositories;

public interface IDeviceRegistrationRepository : IGenericRepository<Guid, DeviceRegistration>
{
}
