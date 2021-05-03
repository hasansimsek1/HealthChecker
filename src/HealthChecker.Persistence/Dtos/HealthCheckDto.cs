namespace HealthChecker.Persistence.Dtos
{



    /// <summary>
    /// DTO form of HealtCheck entity.
    /// </summary>
    public class HealthCheckDto : DtoBase
    {



        /// <summary>
        /// UserId of the entity record.
        /// </summary>
        public string UserId { get; set; }





        /// <summary>
        /// TargetId of the entity record.
        /// </summary>
        public string TargetId { get; set; }





        /// <summary>
        /// Operation status of the health check performed by the job.
        /// </summary>
        public bool IsSuccessful { get; set; }





        /// <summary>
        /// Description of the status of the healt check.
        /// </summary>
        public string StatusExplanation { get; set; }
    }
}
