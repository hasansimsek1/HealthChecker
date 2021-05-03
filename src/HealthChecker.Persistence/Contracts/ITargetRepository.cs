using HealthChecker.Persistence.Dtos;
using System.Threading.Tasks;

namespace HealthChecker.Persistence.Contracts
{
    /// <summary>
    /// A specified repository definition for Target entity.
    /// </summary>
    public interface ITargetRepository
    {
        /// <summary>
        /// This method only updates some of the properties of the Target entity.
        /// </summary>
        /// <param name="targetDto">DTO form of Target entity.</param>
        /// <returns>Task</returns>
        Task UpdatePartially(TargetDto targetDto);
    }
}
