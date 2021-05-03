namespace HealthChecker.Business.Contracts
{
    /// <summary>
    /// Contract for background jobs.
    /// </summary>
    public interface IBackgroundJobService
    {
        /// <summary>
        /// A job must check target URL's status periodically. This method creates or updates a job according to if there is a job with this ID or not.
        /// </summary>
        /// <param name="url">URL of the target that periodically being checked.</param>
        /// <param name="jobId">Identification of the job that we give to job to be able to track that job</param>
        /// <param name="intervalInMinutes">Time interval between periodic checks in minutes. The value should be between 1 and 1440 minutes.</param>
        /// <param name="targetId">ID of the record that user saved to database as a job description.</param>
        /// <param name="userId">ID of the current application user.</param>
        /// <param name="userEmail">Email of the current application user. We will make use of this email to notify if target url is dowsn.</param>
        void SaveHealtCheckJob(string url, string jobId, int intervalInMinutes, string targetId, string userId, string userEmail);

        /// <summary>
        /// Deletes the recurring job with the specified ID.
        /// </summary>
        /// <param name="jobId">ID of the job that is being running in background</param>
        void DeleteRecurringJob(string jobId);
    }
}
