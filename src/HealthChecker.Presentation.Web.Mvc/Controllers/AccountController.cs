using HealthChecker.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HealthChecker.Presentation.Web.Mvc.Controllers
{



    /// <summary>
    /// Authentication operations for the app.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;





        /// <summary>
        /// Constructor for Dependency Injection. Dependencies: UserManager, SignInManager, ILogger.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }





        /// <summary>
        /// Logs the user out.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                string userName = User.Identity.Name;
                await _signInManager.SignOutAsync();
                _logger.LogInformation("{userName} logged out successfully.", userName);
                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Account-Logout.");
                return RedirectToAction("index", "home");
            }
        }





        /// <summary>
        /// Send the login page to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }





        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("{userName} logged in successfully.", User.Identity.Name);
                        return RedirectToAction("index", "dashboard");
                    }

                    string loginResult = $@"IsLockedOut:{result.IsLockedOut}, RequiresTwoFactor:{result.RequiresTwoFactor}, IsNotAllowed:{result.IsNotAllowed}";
                    _logger.LogInformation("Invalid login attempt by {userName}. Login result: {loginResult}", User.Identity.Name, loginResult);

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Account-Login POST action.");
                return RedirectToAction("login", "account");
            }
        }





        /// <summary>
        /// Sends the registration page to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }





        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("{userName} registered successfully.", User.Identity.Name);
                        return RedirectToAction("index", "dashboard");
                    }

                    var errorDesc = "";

                    foreach (var error in result.Errors)
                    {
                        errorDesc += error.Description + " ";
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    _logger.LogInformation("Invalid registration attempt by {userName}. Errors: {errors}", model.Email, errorDesc);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Account-Register POST action.");
                return RedirectToAction("register", "account");
            }
        }





        /// <summary>
        /// Access denied page that is bein used by Identity membership system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
