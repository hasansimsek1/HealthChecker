using System.Collections.Generic;

namespace HealthChecker.Business.ViewModels
{
    /// <summary>
    /// A view-model class that is being used by healt history page.
    /// </summary>
    public class HealthHistoryViewModel
    {
        /// <summary>
        /// The target information whose history will be listed.
        /// </summary>
        public TargetViewModel Target { get; set; }

        /// <summary>
        /// History of the health check results performned by the job.
        /// </summary>
        public List<HealthCheckViewModel> History { get; set; }
    }
}
