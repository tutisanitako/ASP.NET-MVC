using Microsoft.AspNetCore.Mvc;
using tutisani_midterm.Models;
using System.Diagnostics;

namespace tutisani_midterm.Controllers
{
    public class HomeController : Controller
    {
        // Static list to store user data (simulating a database)
        private static List<UserAccountModel> _users = new List<UserAccountModel>
        {
            new UserAccountModel
            {
                UserId = 1,
                FullName = "Anna Carolina",
                Email = "annacarolina@gmail.com",
                Password = "password123",  // Password stored here now
                DateOfBirth = "Not Provided",
                Bio = "Not Provided",
                Disability = "None",
                AccessibilityNeeds = "Not Provided",
                Gender = "Not Provided",
                MobileNumber = "Not Provided",
                EmergencyContact = "",
                Address = "Not Provided"
            },
            new UserAccountModel
            {
                UserId = 2,
                FullName = "John Doe",
                Email = "easyset24@gmail.com",
                Password = "easyset2024",
                DateOfBirth = "01/15/1990",
                Bio = "Travel enthusiast",
                Disability = "None",
                AccessibilityNeeds = "None",
                Gender = "Male",
                MobileNumber = "+1234567890",
                EmergencyContact = "+0987654321",
                Address = "123 Main Street, City"
            },
            new UserAccountModel
            {
                UserId = 3,
                FullName = "test",
                Email = "test@gmail.com",
                Password = "test123",
                DateOfBirth = "07/16/2004",
                Bio = "Chill Guy",
                Disability = "Autistic",
                AccessibilityNeeds = "None",
                Gender = "Female",
                MobileNumber = "+5555555555",
                EmergencyContact = "+77777777",
                Address = "Gau, Tbilisi"
            }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            // Check if model is valid (all validations passed)
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find user by email and verify password
            var user = _users.FirstOrDefault(u =>
                u.Email.Equals(model.Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == model.Password);

            if (user != null)
            {
                // Store UserId in TempData instead of email (more secure and efficient)
                TempData["UserId"] = user.UserId;
                return RedirectToAction("Account");
            }
            else
            {
                // Add error message if credentials are wrong
                ModelState.AddModelError("", "Authorization failed. Invalid email or password.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Account()
        {
            // Get UserId from TempData
            var userId = TempData["UserId"] as int?;

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            // Find user by UserId
            var user = _users.FirstOrDefault(u => u.UserId == userId.Value);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Keep UserId in TempData for future requests
            TempData.Keep("UserId");

            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}