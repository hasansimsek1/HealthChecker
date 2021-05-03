using System.ComponentModel.DataAnnotations;

namespace HealthChecker.Business.ViewModels
{
    /// <summary>
    /// Model for user to registers to the app.
    /// </summary>
    public class RegisterBindingModel
    {
        /// <summary>
        /// Email of the user that is also will be used as username.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
