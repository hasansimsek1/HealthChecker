using System;

namespace HealthChecker.Business.ViewModels
{
    /// <summary>
    /// View model for health check records that are being used in UI.
    /// </summary>
    public class HealthCheckViewModel
    {
        /// <summary>
        /// ID of the user of the health check record.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ID of the Target record.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Information about the health check If the health check was successful or not.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Extra description that may be recorded by the job.
        /// </summary>
        public string StatusExplanation { get; set; }

        /// <summary>
        /// DateTime that job added the record to the database.
        /// </summary>
        public DateTime DateAdded { get; set; }
    }
}
