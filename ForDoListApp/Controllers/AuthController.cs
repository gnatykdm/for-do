using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Service;
using Utills;

namespace ForDoListApp.Controllers
{
    [Route("auth")]
    public class AuthController(IUserService userService, ILogger<AuthController> logger, IHashPassword hashPassword) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IHashPassword _hashPassword = hashPassword;

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View(); 
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            var users = _userService.GetAllUsers();
            if (users == null)
            {
                ViewBag.Error = "No users found.";
                return View();
            }

            var user = users.FirstOrDefault(u => u.Username == username);

            HttpContext.Session.SetInt32("UserId", user.UserId);
            _logger.LogInformation("User {Username} logged in.", username);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            var users = _userService.GetAllUsers();
            if (users == null)
            {
                ViewBag.Error = "No users found.";
                return View();
            }

            var existing = users.FirstOrDefault(u => u.Username == username);

            if (existing != null)
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var newUser = new UserEntity
            {
                Username = username,
                PasswordHash = _hashPassword.HashPasswordSHA256(password)
            };

            _userService.SaveUser(newUser);
            _logger.LogInformation("User {Username} registered.", username);

            return RedirectToAction("Login");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
