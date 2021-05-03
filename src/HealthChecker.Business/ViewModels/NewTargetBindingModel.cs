using HealthChecker.Business.Utilities;
using System.ComponentModel.DataAnnotations;

namespace HealthChecker.Business.ViewModels
{



    /// <summary>
    /// A model that validates incoming data that user newly creates.
    /// </summary>
    public class NewTargetBindingModel
    {



        /// <summary>
        /// Name of the target record. Max lenght is 100 characters. Required to create a target.
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(Name = "Give a name to your URL (max length 100)")]
        public string Name { get; set; }





        /// <summary>
        /// Url for the target that is periodically being checked by a recurring job.
        /// </summary>
        [Required]
        [ValidUrl(ErrorMessage = "Url must be absolute. (e.g. http://www.google.com or https://www.google.com)")]
        [MaxLength(200)]
        [Display(Name = "URL that you want to track. (max length 200)")]
        public string Url { get; set; }





        /// <summary>
        /// Time interval for this new target.
        /// </summary>
        [Required]
        [Display(Name = "Monitoring Interval (1 minute to 1440 minutes)")]
        [Range(1, 1440, ErrorMessage = "Interval range must be between 1 and 1440 minutes")]
        public int MonitoringInterval { get; set; }
    }
}
