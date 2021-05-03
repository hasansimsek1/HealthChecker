namespace HealthChecker.Persistence.Dtos
{
    /// <summary>
    /// DTO form of the Target entity.
    /// </summary>
    public class TargetDto : DtoBase
    {
        /// <summary>
        /// Name of the target entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL of the target entity.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Monitoring interval of the target entity.
        /// </summary>
        public int MonitoringInterval { get; set; }

        /// <summary>
        /// UserId of the target entity.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ID of the job that is bound to the target entity.
        /// </summary>
        public string JobId { get; set; }
    }
}
