using Hangfire.Dashboard;

namespace HealthChecker.Business.Utilities
{
    /// <summary>
    /// Authetication filter for Hangfire Dashboard
    /// </summary>
    public class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Gets fired when a user navigates to the /hangfire URL in the app.
        /// </summary>
        public bool Authorize(DashboardContext context)
        {
            try
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.Identity.IsAuthenticated;
            }
            catch
            {
                return false;
            }
        }
    }
}
