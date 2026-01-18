using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _apiService.PostAsync<UserViewModel>("auth/register", model);

                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("400"))
                {
                    ModelState.AddModelError("", "Email already exists");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _apiService.PostAsync<UserViewModel>("auth/login", model);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                TempData["Success"] = $"Welcome back, {user.FullName}!";

                if (user.IsAdmin)
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return RedirectToAction("Login");

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var orders = await _apiService.GetAsync<List<OrderDetailsViewModel>>($"orders/user/{userId}");
                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load orders: " + ex.Message;
                return View(new List<OrderDetailsViewModel>());
            }
        }
    }
}