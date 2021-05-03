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
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddTargetAsync(NewTargetBindingModel model, ClaimsPrincipal user);





        /// <summary>
        /// Gets all Target records of the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<TargetViewModel>> GetTargetsAsync(ClaimsPrincipal user);





        /// <summary>
        /// Gets the Target record by TargetId and UserId.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<TargetViewModel> GetTargetAsync(string id, ClaimsPrincipal user);





        /// <summary>
        /// Updates the Target of the user and updates the recurring job.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateTargetBindingModel model, ClaimsPrincipal user);





        /// <summary>
        /// Gets the health checks of the target performed before by recurring job.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<HealthCheckViewModel>> GetTargetHealthHistoryAsync(string id, ClaimsPrincipal user);





        /// <summary>
        /// Soft-deletes the target and removes the recurring job.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task DeleteAsync(string id, ClaimsPrincipal user);
    }
}
