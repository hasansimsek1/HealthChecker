using HealthChecker.Business.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HealthChecker.Business.Services
{
    /// <summary>
    /// Implementation of the IUserService interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Gets the user ID from claims. Shorthand method not to hit the database just for a user ID.
        /// </summary>
        public string GetUserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }
    }
}