using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Homework1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public IActionResult Login()
        {
            if (DataStore.CurrentUser != null)
            {
                return RedirectToAction("Index", "Recipe");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.AgreeToTerms)
            {
                ModelState.AddModelError("AgreeToTerms", "You must agree to terms & conditions");
                return View(model);
            }

            var user = DataStore.Users.FirstOrDefault(u =>
                u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            DataStore.CurrentUser = user;
            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index", "Recipe");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            if (DataStore.CurrentUser != null)
            {
                return RedirectToAction("Index", "Recipe");
            }
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.AgreeToTerms)
            {
                ModelState.AddModelError("AgreeToTerms", "You must agree to terms & conditions");
                return View(model);
            }

            if (DataStore.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                AgreeToTerms = model.AgreeToTerms
            };

            DataStore.Users.Add(user);
            DataStore.CurrentUser = user;

            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToAction("Index", "Recipe");
        }

        // GET: Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = DataStore.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return View(model);
            }

            TempData["SuccessMessage"] = "Password reset link has been sent to your email";
            return RedirectToAction("Login");
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            DataStore.CurrentUser = null;
            TempData["SuccessMessage"] = "You have been logged out successfully";
            return RedirectToAction("Login");
        }
    }
}