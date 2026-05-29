using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Infrastructure.Data.Interface
{
	public interface IAppUnitOfWork<T> : IUnitOfWork<T> where T : DbContext
	{

		// MASTER

		// TRANSACTION


		//NOTIFICATION
	}
}
