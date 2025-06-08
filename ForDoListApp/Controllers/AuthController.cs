using ForDoListApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Service;

namespace ForDoListApp.Controllers
{
    [Route("auth")]
    public class AuthController(IUserService userService, ILogger<AuthController> logger) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly HashPassword _hashPassword = new HashPassword();

        private readonly int DEFAULT_LENGTH = 4;

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

            if (username.Length < DEFAULT_LENGTH || password.Length <= DEFAULT_LENGTH)
            {
                ViewBag.Error = "Length of input parameters must be higher then 4 symbols.";
                return View();
            }

            var users = _userService.GetAllUsers();
            if (users == null)
            {
                ViewBag.Error = "No users found.";
                return View();
            }

            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

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
        public IActionResult Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Username, Email and Password are required.";
                return View();
            }

            if (username.Length <= DEFAULT_LENGTH || email.Length <= DEFAULT_LENGTH || password.Length <= DEFAULT_LENGTH)
            {
                ViewBag.Error = "Length of input parameters must be higher then 4 symbols.";
                return View();
            }

            bool isEmailCorrect = ValidateEmail(email);
            if (!isEmailCorrect)
            {
                ViewBag.Error = "Invalid email adress.";
                return View();
            }

            bool isUserWithThisEmailExist = _userService.findUserByEmail(email);
            bool isUserWithThisUsernameExist = _userService.findUserByUserName(username);

            if (isUserWithThisEmailExist || isUserWithThisUsernameExist)
            {
                ViewBag.Error = "User with this email or username already exists.";
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
                Email = email,
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

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                return false;
            }
            return true;
        }
    }
}
