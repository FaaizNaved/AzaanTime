using CommunityToolkit.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NamazTimeApp.Core.Data.Interface;

namespace NamazTimeApp.Core.Data
{
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity>
     where TKey : IEquatable<TKey>
     where TEntity : EntityBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly DbContext _dbContext;

        /// <summary>
        /// The table.
        /// </summary>
        protected readonly DbSet<TEntity> _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TKey, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets entity by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        public virtual TEntity Get(TKey id)
        {
            return _table.Find(id)!;
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>
        /// The entities.
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _table.ToList();
        }

        /// <summary>
        /// Gets the entities by criteria, ordering and paging.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="isOrderAscending">if set to <c>true</c> order ascending.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// The entities.
        /// </returns>
        public virtual IEnumerable<TEntity> GetByCriteria(
            Func<TEntity, bool> filter,
            Func<TEntity, bool> orderBy,
            bool isOrderAscending,
            int pageNumber = 0,
            int pageSize = 10)
        {
            var res = _table.Where(filter).Skip(pageNumber * pageSize).Take(pageSize);
            if (isOrderAscending) res = res.OrderBy(orderBy);
            else res = res.OrderByDescending(orderBy);

            return res.ToList();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The added entity.
        /// </returns>
        public virtual TEntity Add(TEntity entity)
        {
            var now = DateTime.UtcNow;

            entity.CREATED_DATE = now;
            //entity.UPDATED_DATE = now;
            entity.IS_ACTIVE = true;

            var addedEntity = _table.Add(entity);
            return addedEntity.Entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The updated entity.
        /// </returns>
        public virtual TEntity Update(TKey id, TEntity entity)
        {
            var now = DateTime.UtcNow;

            var existing = _table.Find(id);
            if (existing != null)
            {
                entity.CREATED_ID = existing.CREATED_ID;
                entity.CREATED_DATE = existing.CREATED_DATE;
                entity.RECORD_SOURCE_NAME = existing.RECORD_SOURCE_NAME;
                entity.UPDATED_DATE = now;

                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            }
            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TKey id, TEntity entity)
        {
            var existing = _table.Find(id);
            if (existing != null) _table.Remove(existing);
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            Guard.IsNotNull(entities, nameof(entities));

            var list = entities as IList<TEntity> ?? entities.ToList();
            var now = DateTime.UtcNow;

            foreach (var entity in list)
            {
                Guard.IsNotNull(entity, nameof(entities));
                // Assuming these shadow/common fields exist on TEntity via base type or interface.
                entity.CREATED_DATE = now;
                // entity.UPDATED_DATE = now; // if applicable
                entity.IS_ACTIVE = true;
            }

            _table.AddRange(list); // stage all as Added in one call [web:48]
            return list; // same tracked instances; keys populate after SaveChanges [web:22]
        }

        public virtual IQueryable<TEntity> GetAllQueryable()
        {
            return _table;
        }


        #region Async Methods

        public async Task<TEntity?> GetAsync(TKey id, CancellationToken ct = default)
        {
            return await _table.FindAsync(new object[] { id }, ct);
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await _table.ToListAsync(ct);
        }

        public async Task<List<TEntity>> GetByCriteriaAsync(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>>? orderBy,
        bool isAscending,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
        {
            IQueryable<TEntity> query = _table.Where(filter);

            if (orderBy != null)
            {
                query = isAscending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            return await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            entity.CREATED_DATE = now;
            entity.IS_ACTIVE = true;
            entity.RECORD_SOURCE_NAME = Constants.Common.RECORD_SOURCE;

            await _table.AddAsync(entity, ct);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.CREATED_DATE = now;
                entity.IS_ACTIVE = true;
            }

            await _table.AddRangeAsync(entities, ct);
        }

        public void Update(TEntity entity)
        {
            entity.UPDATED_DATE = DateTime.UtcNow;
            _table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return _table.AsQueryable();
        }

        #endregion

    }
}
