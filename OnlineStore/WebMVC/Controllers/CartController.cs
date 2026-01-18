using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApiService _apiService;

        public CartController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString("SessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("SessionId", sessionId);
            }
            return sessionId;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var sessionId = GetSessionId();
                var cartItems = await _apiService.GetAsync<List<CartItemViewModel>>($"cart/{sessionId}");
                return View(cartItems);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load cart: " + ex.Message;
                return View(new List<CartItemViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            try
            {
                var sessionId = GetSessionId();
                var data = new
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId
                };

                await _apiService.PostAsync<CartItemViewModel>("cart", data);
                TempData["Success"] = "Product added to cart!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to add to cart: " + ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            try
            {
                var success = await _apiService.PatchAsync($"cart/{id}", quantity);
                if (success)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update cart" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"cart/{id}");
                if (success)
                {
                    TempData["Success"] = "Item removed from cart!";
                }
                else
                {
                    TempData["Error"] = "Failed to remove item";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to remove item: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}