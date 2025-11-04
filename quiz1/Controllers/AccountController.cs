using Microsoft.AspNetCore.Mvc;
using quiz1.Models;
using quiz1.Services;

namespace quiz1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect to trips
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("MyTrips", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ValidateUser(model.Email, model.Password))
                {
                    var user = _userService.GetUserByEmail(model.Email);
                    HttpContext.Session.SetString("UserEmail", model.Email);

                    // Redirect to CreatePassword only if it's the first login
                    if (user.IsFirstLogin)
                    {
                        return RedirectToAction("CreatePassword");
                    }

                    return RedirectToAction("MyTrips", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePassword()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }
            return View(new CreatePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePassword(CreatePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("UserEmail");
                var user = _userService.GetUserByEmail(email);

                if (user != null)
                {
                    user.Password = model.Password;
                    // Set IsFirstLogin to false after setting the password (for first login or reset)
                    user.IsFirstLogin = false;
                    _userService.UpdateUser(user);
                    // Remain logged in and redirect to MyTrips
                    return RedirectToAction("MyTrips", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByEmail(model.Email);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    // Redirect to CreatePassword for password reset
                    return RedirectToAction("CreatePassword");
                }
                else
                {
                    TempData["Message"] = "If an account exists with this email, follow the steps to reset your password.";
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}