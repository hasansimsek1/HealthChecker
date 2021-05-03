using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecker.Presentation.Web.Mvc.Controllers
{


    public class HomeController : Controller
    {


        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }




        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("index", "dashboard");
            }

            return View();
        }
    }
}
