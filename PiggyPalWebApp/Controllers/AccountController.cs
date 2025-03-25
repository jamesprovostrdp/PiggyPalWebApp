using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
using PiggyPalWebApp.Services;

namespace PiggyPalWebApp.Controllers
{
    public class AccountController : Controller
    {

        [Route("/login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [Route("/register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
    }
}
