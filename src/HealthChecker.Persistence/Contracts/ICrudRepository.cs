using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthChecker.Persistence
{
    /// <summary>
    /// Generic repository pattern definition with CRUD operations to be implemented by the app repositories.
    /// </summary>
    /// 
    /// <para/>
    /// 
    /// Implements : <see cref="IGenericRepository{TEntity, TDto}"/>
    /// 
    /// <para/>
    /// 
    /// <typeparam name="TEntity">Entity that will be processed.</typeparam>
    /// <typeparam name="TDto">Data transfer object form of the entity that carries data.</typeparam>
    public interface ICrudRepository<TEntity, TDto> : IRepository
        where TDto : class, IDtoWithCommonProperties
        where TEntity : class, IEntityWithCommonProperties
    {
        /// <summary>
        /// Retrieves all the entity records.
        /// </summary>
        Task<IEnumerable<TDto>> GetAsync();

        /// <summary>
        /// Retrieves the entity with the specified Id.
        /// </summary>
        /// <param name="Id">Id of the entiry to be retrieved.</param>
        Task<TDto> GetAsync(int Id);

        /// <summary>
        /// Retrieves entities with specified filters.
        /// </summary>
        Task<IEnumerable<TDto>> GetAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Inserts new entity record.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        Task<TDto> InsertAsync(TDto entityDto);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        Task<TDto> UpdateAsync(TDto entityDto);

        /// <summary>
        /// Soft-deletes the entity.
        /// </summary>
        /// <param name="entity">Entity to be soft-deleted.</param>
        Task<TDto> DeleteAsync(TDto entityDto);
    }
}
