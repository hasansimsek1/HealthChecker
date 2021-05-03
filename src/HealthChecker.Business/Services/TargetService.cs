using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthChecker.Business.Contracts;
using HealthChecker.Business.ViewModels;
using HealthChecker.Persistence;
using HealthChecker.Persistence.Contracts;
using HealthChecker.Persistence.Dtos;
using HealthChecker.Persistence.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthChecker.Business.Services
{

    public class TargetService : ITargetService
    {

        private readonly ICrudRepository<Target, TargetDto> _targetRepository;
        private readonly IUserService _userService;
        private readonly ILogger<TargetService> _logger;
        private readonly ICrudRepository<HealthCheck, HealthCheckDto> _healthRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _jobService;
        private readonly ITargetRepository _targetUpdateRepository;

        public TargetService(
            ICrudRepository<Target, TargetDto> targetRepository,
            IUserService userService,
            ILogger<TargetService> logger,
            ICrudRepository<HealthCheck, HealthCheckDto> healthRepository,
            IMapper mapper,
            IBackgroundJobService jobService,
            ITargetRepository targetUpdateRepository)
        {
            _targetRepository = targetRepository;
            _userService = userService;
            _logger = logger;
            _healthRepository = healthRepository;
            _mapper = mapper;
            _jobService = jobService;
            _targetUpdateRepository = targetUpdateRepository;
        }





        /// <summary>
        /// Inserts a new Target record to the database and creates a new recurring job.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddTargetAsync(NewTargetBindingModel model, ClaimsPrincipal user)
        {
            string jobId = Guid.NewGuid().ToString();
            string userId = _userService.GetUserId(user);
            string targetId = Guid.NewGuid().ToString();

            TargetDto targetAppDto = new TargetDto
            {
                Id = targetId,
                MonitoringInterval = model.MonitoringInterval,
                Name = model.Name,
                Url = model.Url,
                UserId = userId,
                JobId = jobId
            };

            _jobService.SaveHealtCheckJob(model.Url, jobId, model.MonitoringInterval, targetId, userId, user.Identity.Name);
            _logger.LogInformation("New job created by {userName}. JobId: {jobId}", user.Identity.Name, jobId);

            await _targetRepository.InsertAsync(targetAppDto);
        }





        /// <summary>
        /// Gets all Target records of the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<TargetViewModel>> GetTargetsAsync(ClaimsPrincipal user)
        {
            // TODO : do TargetDto to TargetViewModel conversion with AutoMapper

            var userId = _userService.GetUserId(user);
            var result = await _targetRepository.GetAsync(x => x.UserId == userId);
            var targetsVM = new List<TargetViewModel>();
            result.OrderByDescending(x => x.DateAdded);

            foreach (var item in result.ToList())
            {
                targetsVM.Add(new TargetViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    MonitoringInterval = item.MonitoringInterval,
                    Url = item.Url,
                    UserId = item.UserId
                });
            }

            return targetsVM;
        }





        /// <summary>
        /// Gets the Target record by TargetId and UserId.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<TargetViewModel> GetTargetAsync(string id, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            var result = await _targetRepository.GetAsync(x => x.Id == id && x.UserId == userId);
            var target = result.FirstOrDefault();

            if (target == null)
            {
                return null;
            }

            TargetViewModel targetViewModel = new TargetViewModel
            {
                Id = target.Id,
                MonitoringInterval = target.MonitoringInterval,
                Name = target.Name,
                Url = target.Url,
                UserId = target.UserId
            };

            return targetViewModel;
        }





        /// <summary>
        /// Updates the Target of the user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdateTargetBindingModel model, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);

            TargetDto targetDto = new TargetDto
            {
                Id = model.Id,
                MonitoringInterval = model.MonitoringInterval,
                Name = model.Name,
                Url = model.Url
            };

            var jobId = await GetJobIdByTargetId(model.Id);
            _jobService.SaveHealtCheckJob(model.Url, jobId, model.MonitoringInterval, model.Id, userId, user.Identity.Name);
            _logger.LogInformation("JobId {jobId} updated by {userName}.", jobId, user.Identity.Name);

            await _targetUpdateRepository.UpdatePartially(targetDto);
        }





        /// <summary>
        /// Get job ID by target ID.
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        private async Task<string> GetJobIdByTargetId(string targetId)
        {
            var target = await _targetRepository.GetAsync(x => x.Id == targetId);
            return target.SingleOrDefault().JobId;
        }





        /// <summary>
        /// Gets the health checks of the target performed before by recurring job.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<HealthCheckViewModel>> GetTargetHealthHistoryAsync(string id, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            var result = await _healthRepository.GetAsync(x => x.TargetId == id && x.UserId == userId);
            return result.AsQueryable().ProjectTo<HealthCheckViewModel>(_mapper.ConfigurationProvider).OrderByDescending(x => x.DateAdded).ToList();
        }





        /// <summary>
        /// Soft-deletes the target and removes the recurring job.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            var result = await _targetRepository.GetAsync(x => x.Id == id && x.UserId == userId);
            var target = result.FirstOrDefault();

            if (target == null)
            {
                return;
            }

            _jobService.DeleteRecurringJob(target.JobId);
            _logger.LogInformation("JobId {jobId} deleted by {userName}.", target.JobId, user.Identity.Name);

            await _targetRepository.DeleteAsync(target);
        }
    }
}
