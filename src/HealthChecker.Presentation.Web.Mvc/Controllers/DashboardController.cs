using HealthChecker.Business.Contracts;
using HealthChecker.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HealthChecker.Presentation.Web.Mvc.Controllers
{



    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITargetService _targetService;
        private readonly ILogger<DashboardController> _logger;

        /// <summary>
        /// Constructor for Dependency Injection system. Dependencies: ITargetService, ILogger.
        /// </summary>
        /// <param name="targetService"></param>
        /// <param name="logger"></param>
        public DashboardController(ITargetService targetService, ILogger<DashboardController> logger)
        {
            _targetService = targetService;
            _logger = logger;
        }





        /// <summary>
        /// Sends the dashboard page to the client.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var targets = await _targetService.GetTargetsAsync(User);
                return View(targets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-Index.");
                return RedirectToAction("Exception", "Error");
            }
        }





        /// <summary>
        /// Sends the new target creation page to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewTarget()
        {
            return View();
        }





        /// <summary>
        /// Creates new target.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NewTarget(NewTargetBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _targetService.AddTargetAsync(model, User);
                    _logger.LogInformation("{userName} added new target {targetName}.", User.Identity.Name, model.Name);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-NewTarget POST action.");
                return RedirectToAction("Exception", "Error");
            }
        }





        /// <summary>
        /// Sends target detail page to the client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Target(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
                {
                    return RedirectToAction("index");
                }

                var target = await _targetService.GetTargetAsync(id, User);

                UpdateTargetBindingModel updateTargetBindingModel = new UpdateTargetBindingModel
                {
                    Id = target.Id,
                    MonitoringInterval = target.MonitoringInterval,
                    Name = target.Name,
                    Url = target.Url
                };

                return View(updateTargetBindingModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-Target GET action.");
                return RedirectToAction("Exception", "Error");
            }
        }





        /// <summary>
        /// Updates the specified target.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Target(UpdateTargetBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _targetService.UpdateAsync(model, User);
                    _logger.LogInformation("{userName} updated target {targetName}.", User.Identity.Name, model.Name);
                    return RedirectToAction("index");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-Target POST action.");
                return RedirectToAction("Exception", "Error");
            }
        }





        /// <summary>
        /// Sends target health check history page to the client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TargetHealthHistory(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
                {
                    return RedirectToAction("index");
                }

                var historyData = await _targetService.GetTargetHealthHistoryAsync(id, User);
                var targetData = await _targetService.GetTargetAsync(id, User);

                return View(new HealthHistoryViewModel
                {
                    History = historyData,
                    Target = targetData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-TargetHealthHistory action.");
                return RedirectToAction("Exception", "Error");
            }
        }





        /// <summary>
        /// Deletes the specified target.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteTarget(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
                {
                    return RedirectToAction("index");
                }

                await _targetService.DeleteAsync(id, User);
                _logger.LogInformation("{userName} deleted TargetId {targetId}.", User.Identity.Name, id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Dashboard-DeleteTarget action.");
                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
