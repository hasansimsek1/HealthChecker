using HealthChecker.Business.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthChecker.Business.Contracts
{
    /// <summary>
    /// Actions related to the Target entity that app users save as jobs.
    /// </summary>
    public interface ITargetService
    {
        /// <summary>
        /// Inserts a new Target record to the database and creates a new recurring job.
        /// </summary>
        Task AddTargetAsync(NewTargetBindingModel model, ClaimsPrincipal user);

        /// <summary>
        /// Gets all Target records of the specified user.
        /// </summary>
        Task<List<TargetViewModel>> GetTargetsAsync(ClaimsPrincipal user);

        /// <summary>
        /// Gets the Target record by TargetId and UserId.
        /// </summary>
        Task<TargetViewModel> GetTargetAsync(string id, ClaimsPrincipal user);

        /// <summary>
        /// Updates the Target of the user and updates the recurring job.
        /// </summary>
        Task UpdateAsync(UpdateTargetBindingModel model, ClaimsPrincipal user);

        /// <summary>
        /// Gets the health checks of the target performed before by recurring job.
        /// </summary>
        Task<List<HealthCheckViewModel>> GetTargetHealthHistoryAsync(string id, ClaimsPrincipal user);

        /// <summary>
        /// Soft-deletes the target and removes the recurring job.
        /// </summary>
        Task DeleteAsync(string id, ClaimsPrincipal user);
    }
}
