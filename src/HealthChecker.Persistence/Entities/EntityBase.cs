using System;

namespace HealthChecker.Persistence.Entities
{



    /// <summary>
    /// Base class for the entities that have common properties.
    /// 
    /// <para/>
    /// 
    /// Implements : <see cref="IEntityWithCommonProperties"/>
    /// <para/>
    /// 
    /// Properties :
    /// <para/><see cref="Id"/> <see cref="string"/> (no attributes) (default value = <see cref="Guid.NewGuid"/>)
    /// <para/><see cref="IsDeleted"/> <see cref="bool"/> (no attributes) (default value = <see cref="false"/>)
    /// <para/><see cref="DateAdded"/> <see cref="DateTime"/> (no attributes) (default value = <see cref="DateTime.Now"/>)
    /// <para/><see cref="DateModified"/> <see cref="DateTime"/> (no attributes) (default value = <see cref="DateTime.Now"/>)
    /// 
    /// </summary>

    public class EntityBase : IEntityWithCommonProperties
    {



        /// <summary>
        /// Id of the entity. Initialized with auto-property initializer with value of <see cref="Guid.NewGuid"/>
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();





        /// <summary>
        /// Creation date of the entity. Initialized with auto-property initializer with value of <see cref="DateTime.Now"/>
        /// </summary>
        public DateTime DateAdded { get; set; } = DateTime.Now;





        /// <summary>
        /// Modification date of the entity. Initialized with auto-property initializer with value of <see cref="DateTime.Now"/>
        /// </summary>
        public DateTime DateModified { get; set; } = DateTime.Now;





        /// <summary>
        /// Soft-delete operation status of the entity. Initialized with auto-property initializer with value of <see cref="false"/>
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
