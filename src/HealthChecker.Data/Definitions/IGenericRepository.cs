namespace HealthChecker.Data.Definitions
{
    /// <summary>
    /// Base criterias of generic repositories.
    /// </summary>
    public interface IGenericRepository<TEntity, TDto> : IRepository
        where TDto : class, IDto
        where TEntity : class, IEntity
    {
    }
}
