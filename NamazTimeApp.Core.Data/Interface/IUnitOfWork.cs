using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace NamazTimeApp.Core.Data.Interface
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        /// <summary>
        /// Persist all changes into persistence as single operation.
        /// </summary>
        /// <returns>Number of record that changed.</returns>

        T Context { get; }
        int SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
