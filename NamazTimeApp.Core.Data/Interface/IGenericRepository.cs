using System.Linq.Expressions;

namespace NamazTimeApp.Core.Data.Interface
{
    public interface IGenericRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase
    {
        /// <summary>
        /// Gets entity by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>
        /// The entities.
        /// </returns>
        IEnumerable<TEntity> GetAll();

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
        IEnumerable<TEntity> GetByCriteria(
            Func<TEntity, bool> filter,
            Func<TEntity, bool> orderBy,
            bool isOrderAscending,
            int pageNumber,
            int pageSize);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The added entity.
        /// </returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The updated entity.
        /// </returns>
        TEntity Update(TKey id, TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        void Delete(TKey id, TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetAllQueryable();

        #region Async

        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetByCriteriaAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>>? orderBy,
            bool isAscending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Query();

        #endregion
    }
}
