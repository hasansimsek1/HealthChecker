namespace HealthChecker.Persistence.Entities
{
    /// <summary>
    /// An entity class reflected to the database. This entity keeps records about the health checks that performed by the recurring job.
    /// </summary>
    public class HealthCheck : EntityBase
    {
        /// <summary>
        /// ID of the user who owns this record.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ID of the Target entity that user saved to database to be able to track health status.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// A boolean record to track if the health check was successful or not.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Description about the status of the health check performed by the job.
        /// </summary>
        public string StatusExplanation { get; set; }
    }
}
