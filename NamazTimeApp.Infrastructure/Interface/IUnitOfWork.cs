using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core.Data.Interface;
using NamazTimeApp.Master.Repositories;
using NamazTimeApp.Notification.Repositories;
using NamazTimeApp.Transaction.Repositories;

namespace NamazTimeApp.Infrastructure.Data.Interface
{
	public interface IAppUnitOfWork<T> : IUnitOfWork<T> where T : DbContext
	{

		// MASTER
		ILocationRepository Locations { get; }
		IPrayerTypeRepository PrayerTypes { get; }
		IAppSettingRepository AppSettings { get; }

		// TRANSACTION
		IPrayerTimeRepository PrayerTimes { get; }
		IDeviceRegistrationRepository DeviceRegistrations { get; }


		//NOTIFICATION
		INotificationLogRepository NotificationLogs { get; }
	}
}
