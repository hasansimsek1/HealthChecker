using System;

namespace HealthChecker.Data.Definitions
{
    /// <summary>
    /// Definitions of this interface are being used in ICrudRepository implementations to be able to set common properties automatically (like AddedDate, ModifiedDate etc).
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
