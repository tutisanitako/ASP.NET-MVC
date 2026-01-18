using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApiService _apiService;

        public AdminController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _apiService.GetAsync<List<ProductViewModel>>("products");
                return View(products);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load products: " + ex.Message;
                return View(new List<ProductViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _apiService.PostAsync<ProductViewModel>("products", model);
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to create product: " + ex.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _apiService.GetAsync<ProductViewModel>($"products/{id}");
                var model = new CreateProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Stock = product.Stock,
                    Category = product.Category
                };
                ViewBag.ProductId = id;
                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Product not found";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = id;
                return View(model);
            }

            try
            {
                var success = await _apiService.PutAsync($"products/{id}", model);
                if (success)
                {
                    TempData["Success"] = "Product updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Failed to update product";
                    ViewBag.ProductId = id;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to update product: " + ex.Message;
                ViewBag.ProductId = id;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"products/{id}");
                if (success)
                {
                    TempData["Success"] = "Product deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to delete product";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to delete product: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Orders()
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderDetailsViewModel>>("orders");
                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load orders: " + ex.Message;
                return View(new List<OrderDetailsViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            try
            {
                var success = await _apiService.PatchAsync($"orders/{id}/status", status);
                if (success)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Failed to update status" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("auth/users");
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load users: " + ex.Message;
                return View(new List<UserViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"auth/{id}");
                if (success)
                {
                    TempData["Success"] = "User deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to delete user";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to delete user: " + ex.Message;
            }

            return RedirectToAction("Users");
        }
    }
}