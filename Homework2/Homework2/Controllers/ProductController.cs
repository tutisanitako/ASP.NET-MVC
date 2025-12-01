using Homework2.Data;
using Homework2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId, decimal? minPrice, decimal? maxPrice, string sortBy = "name", int page = 1)
        {
            int pageSize = 6;

            // Get all products with categories
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Apply filters
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name)
            };

            // Get total count before pagination
            int totalProducts = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Apply pagination
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calculate statistics for all products (not just filtered)
            var allProducts = await _context.Products.ToListAsync();
            decimal totalCost = allProducts.Sum(p => p.Price);
            decimal averagePrice = allProducts.Any() ? allProducts.Average(p => p.Price) : 0;

            // Pass data to view
            ViewBag.PageTitle = "Product Management System";
            ViewBag.TotalProducts = allProducts.Count;
            ViewBag.TotalCost = totalCost;
            ViewBag.AveragePrice = averagePrice;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.CategoryId = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortBy = sortBy;

            ViewData["Categories"] = await _context.Categories.ToListAsync();

            // Display filter message if filters are applied
            if (categoryId.HasValue || minPrice.HasValue || maxPrice.HasValue)
            {
                var categoryName = categoryId.HasValue ?
                    (await _context.Categories.FindAsync(categoryId.Value))?.Name : "All";
                TempData["FilterMessage"] = $"Found {totalProducts} products" +
                    (categoryId.HasValue ? $" in {categoryName} category" : "") +
                    (minPrice.HasValue || maxPrice.HasValue ? $" with price range {minPrice ?? 0:C} - {maxPrice?.ToString("C") ?? "∞"}" : "");
            }
            else if (TempData["WelcomeMessage"] == null)
            {
                TempData["WelcomeMessage"] = "Welcome! Browse our complete product catalog.";
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Product ID is required.";
                return RedirectToAction(nameof(Index));
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Successfully loaded details for '{product.Name}'.";
            return View(product);
        }

        [HttpPost]
        public IActionResult ClearFilters()
        {
            TempData["WelcomeMessage"] = "All filters have been cleared.";
            return RedirectToAction(nameof(Index));
        }
    }
}