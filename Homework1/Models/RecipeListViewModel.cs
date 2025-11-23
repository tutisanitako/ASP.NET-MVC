using System.Collections.Generic;

namespace Homework1.Models
{
    public class RecipeListViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}