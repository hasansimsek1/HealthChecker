namespace HealthChecker.Persistence.Entities
{
    /// <summary>
    /// An entity class that user interracts with database to create a recurring health check job.
    /// </summary>
    public class Target : EntityBase
    {
        /// <summary>
        /// Name of the entity that user defines.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL of the Target entity that user defines.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Monitoring interval period in minutes that is being used by the recurring job. User defines values between 1 and 1440 minutes.
        /// </summary>
        public int MonitoringInterval { get; set; }

        /// <summary>
        /// ID of the owner of the Target record.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// A GUID that application gives to the job to be able to track the job.
        /// </summary>
        public string JobId { get; set; }
    }
}
