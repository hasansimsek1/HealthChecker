namespace HealthChecker.Business.ViewModels
{



    /// <summary>
    /// A general target view-model
    /// </summary>
    public class TargetViewModel
    {



        /// <summary>
        /// ID of the target.
        /// </summary>
        public string Id { get; set; }





        /// <summary>
        /// Name of the target.
        /// </summary>
        public string Name { get; set; }





        /// <summary>
        /// Url of the target.
        /// </summary>
        public string Url { get; set; }





        /// <summary>
        /// Monitoring interval of the job.
        /// </summary>
        public int MonitoringInterval { get; set; }





        /// <summary>
        /// ID of the user of the target.
        /// </summary>
        public string UserId { get; set; }
    }
}
