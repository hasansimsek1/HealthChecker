using System.ComponentModel.DataAnnotations;

namespace HealthChecker.Business.ViewModels
{
    /// <summary>
    /// A model that verifies incoming login information from the UI.
    /// </summary>
    public class LoginBindingModel
    {
        /// <summary>
        /// Email address of the user that is also being used as username.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Login password of the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
