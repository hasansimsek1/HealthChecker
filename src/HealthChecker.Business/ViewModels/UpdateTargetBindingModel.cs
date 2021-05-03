using HealthChecker.Business.Utilities;
using System.ComponentModel.DataAnnotations;

namespace HealthChecker.Business.ViewModels
{



    /// <summary>
    /// A view-model that is being used in the update target page.
    /// </summary>
    public class UpdateTargetBindingModel
    {



        /// <summary>
        /// Id of the target that will be updated.
        /// </summary>
        [Required]
        public string Id { get; set; }





        /// <summary>
        /// Name of the target.
        /// </summary>
        [Required]
        [Display(Name = "Give a name to your URL")]
        public string Name { get; set; }





        /// <summary>
        /// Url of the target.
        /// </summary>
        [Required]
        [ValidUrl(ErrorMessage = "Url must be absolute. (e.g. http://www.google.com or https://www.google.com)")]
        public string Url { get; set; }





        /// <summary>
        /// Monitoring interval for the job.
        /// </summary>
        [Required]
        [Display(Name = "Monitoring interval (1 minute to 1440 minutes)")]
        public int MonitoringInterval { get; set; }
    }
}
