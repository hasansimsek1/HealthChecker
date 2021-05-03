using Hangfire;
using HealthChecker.Business.Contracts;
using HealthChecker.Persistence;
using HealthChecker.Persistence.Dtos;
using HealthChecker.Persistence.Entities;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HealthChecker.Business.Services
{



    /// <summary>
    /// Implementation of IBackgroundJobService interface for Hangfire library.
    /// </summary>
    public class HangfireJobService : IBackgroundJobService
    {



        /// <summary>
        /// Built in Logger interface that injected to here.
        /// </summary>
        private readonly ILogger<HangfireJobService> _logger;





        /// <summary>
        /// Injected health repository to be able to interract with database records.
        /// </summary>
        private readonly ICrudRepository<HealthCheck, HealthCheckDto> _healthRepository;





        /// <summary>
        /// Constructor that injects logger and health repository
        /// </summary>
        /// <param name="logger">Injected logger that is configured in DI system.</param>
        /// <param name="healthRepository">Injected repository of health check related records.</param>
        public HangfireJobService(ILogger<HangfireJobService> logger, ICrudRepository<HealthCheck, HealthCheckDto> healthRepository)
        {
            _logger = logger;
            _healthRepository = healthRepository;
        }





        /// <summary>
        /// If there is no existing job with the specified jobId creates new one. If There is existing job, it updates the existing.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jobId"></param>
        /// <param name="intervalInMinutes"></param>
        /// <param name="targetId"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        public void SaveHealtCheckJob(string url, string jobId, int intervalInMinutes, string targetId, string userId, string userEmail)
        {
            try
            {
                RecurringJob.AddOrUpdate(jobId, () => Request(url, targetId, userId, userEmail), Cron.MinuteInterval(intervalInMinutes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while saving recurring job. TargetId: {targetId}, UserId: {userId}, Url: {url},", targetId, userId, url);
                throw;
            }
        }





        /// <summary>
        /// Deletes the recurring job with the specified jobId.
        /// </summary>
        /// <param name="jobId"></param>
        public void DeleteRecurringJob(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);
        }





        /// <summary>
        /// The method that is being periodically fired by Hangfire. It makes HTTP request to the specified URL. If response is successful, it will insert a success record to the database. If response is not successful then it sends email to the specified userEmail and inserts error record to the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="targetId"></param>
        /// <param name="userId"></param>
        /// <param name="userEmail"></param>
        [AutomaticRetry(Attempts = 0)]
        public void Request(string url, string targetId, string userId, string userEmail)
        {
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = restClient.ExecuteAsync(request);

            if (response.Wait(TimeSpan.FromSeconds(45)))
            {
                var responseStatus = response.Result;

                if (responseStatus.IsSuccessful)
                {
                    InsertHealthCheckRecord(true, targetId, userId, "Success");
                    _logger.LogInformation("Target returned success result for {targetUrl}", url);
                    return;
                }
            }

            BackgroundJob.Enqueue(() => SendEmail(url, userEmail));
            _logger.LogError("Recurring job error for URL {url}", url);
            InsertHealthCheckRecord(false, targetId, userId, "Failure");
        }





        /// <summary>
        /// Helper method that job inserts success or failure record to the database.
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="targetId"></param>
        /// <param name="userId"></param>
        /// <param name="description"></param>
        private void InsertHealthCheckRecord(bool isSuccess, string targetId, string userId, string description)
        {
            _healthRepository.InsertAsync(
            new HealthCheckDto
            {
                IsSuccessful = isSuccess,
                TargetId = targetId,
                UserId = userId,
                StatusExplanation = description //$"Failure. StatusCode:{response.Result.StatusCode}"
            });
        }





        /// <summary>
        /// Sends an allert indicating an error to the specified email. This method is being fired by the health check job.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="emailTo"></param>
        public void SendEmail(string url, string emailTo)
        {
            try
            {
                NetworkCredential credentials = new NetworkCredential("healthchecker007@gmail.com", "healthcheck.007");

                using SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                using MailMessage msg = new MailMessage
                {
                    From = new MailAddress("healthchecker007@gmail.com"),
                    Subject = "Your website is down!",
                    Body = $"We could not reach your website ({url}) at " + DateTime.Now.ToString(),
                    IsBodyHtml = true,
                    HeadersEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8,
                };
                msg.To.Add(new MailAddress(emailTo));

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }
            catch
            {
                throw;
            }
        }
    }
}
