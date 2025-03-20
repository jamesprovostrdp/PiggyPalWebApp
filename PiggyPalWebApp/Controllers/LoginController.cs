using Microsoft.AspNetCore.Mvc;

namespace PiggyPalWebApp.Controllers
{
    public class LoginController : Controller
    {
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
