using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
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

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _apiService.GetAsync<ProductViewModel>($"products/{id}");
                return View(product);
            }
            catch (Exception)
            {
                TempData["Error"] = "Product not found";
                return RedirectToAction("Index");
            }
        }
    }
}