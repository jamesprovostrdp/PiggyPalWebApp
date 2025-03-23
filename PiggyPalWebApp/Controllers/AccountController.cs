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

        private DatabaseContext _context { get; set; }

        private TokenService _tokenService { get; set; }

        public AccountController(DatabaseContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDataModel userRegister)
        {
            // Create a user object with use information
            User user = new()
            {
                Username = userRegister.Username,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password),
                Email = userRegister.Email,
                Role = userRegister.Role
            };

            // Add user to database and save
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return 200 and confrimation
            return Ok("User registered");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthDataModel userAuth)
        {
            // Get user based on inputed username
            User? user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userAuth.Username);

            // If username isnt found compare to emails instead
            user ??= await _context.Users.SingleOrDefaultAsync(u => u.Email == userAuth.Username);

            // If credentials dont match or do not exist return 401
            if (user == null || !BCrypt.Net.BCrypt.Verify(userAuth.Password, user.HashPassword))
            {
                return Unauthorized("Invalid Credentials");
            }

            // Generate the token based on user's data
            var token = _tokenService.GenerateToken(user.Username, user.Role.ToString());

            // Return 200 with the token
            return Ok(new { Token = token });
        }
    }
}
