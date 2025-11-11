using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lecture9.Models;


namespace Lecture9.Controllers
{
    public class HomeController : Controller
    {
        // Static list to store products (simulating database)
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics", Description = "High performance laptop" },
            new Product { Id = 2, Name = "Mouse", Price = 25.50m, Category = "Electronics", Description = "Wireless mouse" },
            new Product { Id = 3, Name = "Desk", Price = 299.99m, Category = "Furniture", Description = "Office desk" }
        };

        // GET: Home/Index - Product List
        public IActionResult Index()
        {
            // Use ViewModel to pass main data
            var viewModel = new ProductListViewModel
            {
                Products = products,
                TotalProducts = products.Count
            };

            // Check if there's a success message from TempData
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(viewModel);
        }

        // GET: Home/Create - Add Product Form
        public IActionResult Create()
        {
            // Use ViewData to pass page title
            ViewData["PageTitle"] = "Add Product";

            // Use ViewBag to pass category list for dropdown
            ViewBag.Categories = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select Category", Value = "" },
                new SelectListItem { Text = "Electronics", Value = "Electronics" },
                new SelectListItem { Text = "Furniture", Value = "Furniture" },
                new SelectListItem { Text = "Clothing", Value = "Clothing" },
                new SelectListItem { Text = "Books", Value = "Books" },
                new SelectListItem { Text = "Food", Value = "Food" }
            };

            return View();
        }

        // POST: Home/Create - Save Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Generate new ID
                product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;

                // Add product to list
                products.Add(product);

                // Use TempData to store success message for redirect
                TempData["SuccessMessage"] = "Product added successfully";

                // Redirect to Index page
                return RedirectToAction("Index");
            }

            // If validation fails, reload the form with categories
            ViewData["PageTitle"] = "Add Product";
            ViewBag.Categories = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select Category", Value = "" },
                new SelectListItem { Text = "Electronics", Value = "Electronics" },
                new SelectListItem { Text = "Furniture", Value = "Furniture" },
                new SelectListItem { Text = "Clothing", Value = "Clothing" },
                new SelectListItem { Text = "Books", Value = "Books" },
                new SelectListItem { Text = "Food", Value = "Food" }
            };

            return View(product);
        }

        // GET: Home/Details - Product Details
        public IActionResult Details(int id)
        {
            // Find the product
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Use ViewData to pass text header
            ViewData["HeaderText"] = "View Product Details";

            // Use ViewBag to pass current time
            ViewBag.CurrentTime = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm:ss tt");

            // Use ViewModel (Product model) to pass main data
            return View(product);
        }
    }
}