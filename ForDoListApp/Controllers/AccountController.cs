using Microsoft.AspNetCore.Mvc;

namespace Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        // TODO: Add your authentication logic here

        // For now, just redirect to Home or reload the page
        if (IsValidUser(Email, Password))
        {
            // Example: Redirect to Dashboard after successful login
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Optionally add ModelState error to show login failure in the view
            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }
    }

    private bool IsValidUser(string email, string password)
    {
        // Replace with your user validation logic
        return (email == "test@example.com" && password == "password123");
    }
}
