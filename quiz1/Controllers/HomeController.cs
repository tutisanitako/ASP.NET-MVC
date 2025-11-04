using Microsoft.AspNetCore.Mvc;
using quiz1.Models;
using quiz1.Services;
using System.Diagnostics;

namespace quiz1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITripService _tripService;

        public HomeController(ILogger<HomeController> logger, ITripService tripService)
        {
            _logger = logger;
            _tripService = tripService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("MyTrips");
        }

        public IActionResult MyTrips()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var trips = _tripService.GetUserTrips(email);
            ViewBag.UserEmail = email;
            return View(trips);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}