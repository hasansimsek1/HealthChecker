using System.Security.Claims;

namespace HealthChecker.Business.Contracts
{



    /// <summary>
    /// Contract for user related methods.
    /// </summary>
    public interface IUserService
    {



        /// <summary>
        /// Gets the user ID from claims. Shorthand method not to hit the database for just a user ID.
        /// </summary>
        /// <param name="user">Claims of the user.</param>
        /// <returns>string - ID of the current user.</returns>
        string GetUserId(ClaimsPrincipal user);
    }
}
