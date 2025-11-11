using Lecture9.Models;
using Microsoft.AspNetCore.Mvc;


namespace Lecture9.Controllers
{
    public class ProductsController : Controller
    {
        // Static list to store products (acts as in-memory database)
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Dell", Price = 13500, Category = "Electronics", Description = "High performance laptop", CreatedDate = DateTime.Now.AddDays(-5) },
            new Product { Id = 2, Name = "iPhone 15", Price = 25500, Category = "Electronics", Description = "Latest smartphone", CreatedDate = DateTime.Now.AddDays(-3) },
            new Product { Id = 3, Name = "Office Chair", Price = 15500, Category = "Furniture", Description = "Ergonomic office chair", CreatedDate = DateTime.Now.AddDays(-1) }
        };

        private static int _nextId = 4;

        // GET: Products/Index
        public IActionResult Index()
        {
            var viewModel = new ProductListViewModel
            {
                Products = _products,
                TotalProducts = _products.Count,
                TotalValue = _products.Sum(p => p.Price)
            };

            // Display success message from TempData
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(viewModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            // ViewBag for Category dropdown
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Clothing",
                "Food",
                "Books",
                "Furniture",
                "Toys"
            };

            // ViewData for page title
            ViewData["PageTitle"] = "Add Product";

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = _nextId++;
                product.CreatedDate = DateTime.Now;
                _products.Add(product);

                // Use TempData for success message
                TempData["SuccessMessage"] = "Product added successfully";

                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag if validation fails
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Clothing",
                "Food",
                "Books",
                "Furniture",
                "Toys"
            };

            ViewData["PageTitle"] = "Add Product";

            return View(product);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // ViewData for text
            ViewData["HeaderText"] = "View Product Details";

            // ViewBag for current time
            ViewBag.CurrentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // ViewBag for Category dropdown
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Clothing",
                "Food",
                "Books",
                "Furniture",
                "Toys"
            };

            // ViewData for page title
            ViewData["PageTitle"] = "Edit Product";

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Update the product
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
                existingProduct.Description = product.Description;

                TempData["SuccessMessage"] = "Product updated successfully";

                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag if validation fails
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Clothing",
                "Food",
                "Books",
                "Furniture",
                "Toys"
            };

            ViewData["PageTitle"] = "Edit Product";

            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewData["PageTitle"] = "Delete Product";

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _products.Remove(product);
                TempData["SuccessMessage"] = "Product deleted successfully";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}