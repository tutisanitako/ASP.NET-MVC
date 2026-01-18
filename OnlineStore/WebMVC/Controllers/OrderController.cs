using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApiService _apiService;

        public OrderController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private string GetSessionId()
        {
            return HttpContext.Session.GetString("SessionId") ?? "";
        }

        public IActionResult Checkout()
        {
            var model = new OrderViewModel();

            // Pre-fill if user is logged in
            if (User.Identity?.IsAuthenticated ?? false)
            {
                model.CustomerName = User.FindFirstValue(ClaimTypes.Name) ?? "";
                model.CustomerEmail = User.FindFirstValue(ClaimTypes.Email) ?? "";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var sessionId = GetSessionId();
                var userId = User.Identity?.IsAuthenticated ?? false
                    ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")
                    : (int?)null;

                var data = new
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    Address = model.Address,
                    Phone = model.Phone,
                    SessionId = sessionId,
                    UserId = userId
                };

                var order = await _apiService.PostAsync<OrderDetailsViewModel>("orders", data);
                TempData["OrderId"] = order.Id;
                TempData["TrackingCode"] = order.TrackingCode;
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to place order: " + ex.Message;
                return View(model);
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Track(string code)
        {
            if (string.IsNullOrEmpty(code))
                return View("TrackOrder");

            try
            {
                var order = await _apiService.GetAsync<OrderDetailsViewModel>($"orders/tracking/{code}");
                return View("OrderDetails", order);
            }
            catch (Exception)
            {
                TempData["Error"] = "Order not found";
                return View("TrackOrder");
            }
        }

        [HttpGet]
        public IActionResult TrackOrder()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _apiService.GetAsync<OrderDetailsViewModel>($"orders/{id}");
                return View("OrderDetails", order);
            }
            catch (Exception)
            {
                TempData["Error"] = "Order not found";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}