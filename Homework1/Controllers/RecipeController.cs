using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Homework1.Controllers
{
    public class RecipeController : Controller
    {
        private const int PageSize = 3;

        // GET: Recipe/Index
        public IActionResult Index(int page = 1)
        {
            if (DataStore.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var totalRecipes = DataStore.Recipes.Count;
            var totalPages = (int)Math.Ceiling(totalRecipes / (double)PageSize);

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var recipes = DataStore.Recipes
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var viewModel = new RecipeListViewModel
            {
                Recipes = recipes,
                CurrentPage = page,
                TotalPages = totalPages
            };

            ViewBag.UserName = DataStore.CurrentUser.Name;
            return View(viewModel);
        }

        // GET: Recipe/Details/5
        public IActionResult Details(int id)
        {
            if (DataStore.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var recipe = DataStore.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            ViewBag.UserName = DataStore.CurrentUser.Name;
            return View(recipe);
        }

        // POST: Recipe/ToggleLike
        [HttpPost]
        public IActionResult ToggleLike(int id)
        {
            if (DataStore.CurrentUser == null)
            {
                return Json(new { success = false, message = "Not authenticated" });
            }

            DataStore.ToggleLike(id);
            var recipe = DataStore.GetRecipeById(id);

            return Json(new { success = true, isLiked = recipe.IsLiked });
        }

        // POST: Recipe/ToggleBookmark
        [HttpPost]
        public IActionResult ToggleBookmark(int id)
        {
            if (DataStore.CurrentUser == null)
            {
                return Json(new { success = false, message = "Not authenticated" });
            }

            DataStore.ToggleBookmark(id);
            var recipe = DataStore.GetRecipeById(id);

            return Json(new { success = true, isBookmarked = recipe.IsBookmarked });
        }
    }
}