using System;

namespace HealthChecker.Persistence
{
    /// <summary>
    /// Definitions of this interface are being used by ICrudRepository implementations to be able to set common properties automatically (Id, DateAdded, DateModified, IsDeleted).
    /// </summary>
    public interface IEntityWithCommonProperties : IEntity
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Creation date of the entity.
        /// </summary>
        DateTime DateAdded { get; set; }

        /// <summary>
        /// Modified date of the entity.
        /// </summary>
        DateTime DateModified { get; set; }

        /// <summary>
        /// Soft-delete status of the entity.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
