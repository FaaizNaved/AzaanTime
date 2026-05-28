using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Core.Data
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        /// <summary>
        /// Indicates the object is already disposed.
        /// </summary>
        protected bool _isDisposed;

        /// <summary>
        /// Hold the database context.
        /// </summary>
        protected DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{T}" /> class.
        /// </summary>
        /// <param name="dbContext">The DB context.</param>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T Context => (T)_dbContext;
        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork{T}"/> class.
        /// </summary>
        /// 

        ~UnitOfWork()
        {
            Dispose(true);
        }

        /// <summary>
        /// Persist all changes into persistence as single operation.
        /// </summary>
        /// <returns>
        /// Number of record that changed.
        /// </returns>
        /// 

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous save operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
        /// written to the database.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> indicates the disposed is called from Dispose method; <c>false</c> indicates that the method is called from finalize method.</param>
        private void Dispose(bool disposing)
        {
            // Already been disposed, then do nothing
            if (_isDisposed)
            {
                return;
            }

            // Release all unmanaged resources
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }

            _isDisposed = true;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
    }
}
