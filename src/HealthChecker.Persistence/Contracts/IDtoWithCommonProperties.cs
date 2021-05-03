using System;

namespace HealthChecker.Persistence
{
    /// <summary>
    /// Contract for data transfer objects containing common properties.
    /// </summary>
    public interface IDtoWithCommonProperties : IDto
    {
        /// <summary>
        /// Id of the related entity.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Creation date of the related entity.
        /// </summary>
        DateTime DateAdded { get; set; }

        /// <summary>
        /// Modification date of the related entity.
        /// </summary>
        DateTime DateModified { get; set; }

        /// <summary>
        /// Soft-delete status of the related entity.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
