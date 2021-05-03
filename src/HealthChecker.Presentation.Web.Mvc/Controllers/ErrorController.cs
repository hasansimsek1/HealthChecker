using Microsoft.AspNetCore.Mvc;

namespace HealthChecker.Presentation.Web.Mvc.Controllers
{



    /// <summary>
    /// Central error pages.
    /// </summary>
    public class ErrorController : Controller
    {



        /// <summary>
        /// User sees this page when navigates to a unknown url in the app.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Error/NotFound")]
        public IActionResult UrlNotFound()
        {
            return View();
        }





        /// <summary>
        /// General exception page that is being send to the client when unexpected errors occur.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Error/Exception")]
        public IActionResult Exception()
        {
            return View();
        }
    }
}
