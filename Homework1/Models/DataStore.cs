using System.Collections.Generic;
using System.Linq;

namespace Homework1.Models
{
    public static class DataStore
    {
        static DataStore()
        {
            // Initialize with premade users
            Users = new List<User>
            {
                new User
                {
                    Name = "test",
                    Email = "test@gmail.com",
                    Password = "test123",
                    AgreeToTerms = true
                },
                new User
                {
                    Name = "John Doe",
                    Email = "john@example.com",
                    Password = "password123",
                    AgreeToTerms = true
                },
                new User
                {
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    Password = "password123",
                    AgreeToTerms = true
                }
            };
        }

        public static List<User> Users { get; set; }
        public static User CurrentUser { get; set; }

        public static List<Recipe> Recipes { get; set; } = new List<Recipe>
        {
            new Recipe
            {
                Id = 1,
                Title = "Fresh Garden Salad",
                MadeBy = "John Doe",
                ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400&h=400&fit=crop",
                Ingredients = "Mixed greens, tomatoes, cucumbers, olive oil, lemon juice, salt, pepper",
                Description = "A refreshing and healthy salad perfect for any meal",
                MakingProcess = "Wash all vegetables thoroughly. Chop tomatoes and cucumbers. Mix greens in a large bowl. Add chopped vegetables. Drizzle with olive oil and lemon juice. Season with salt and pepper. Toss well and serve.",
                VideoLink = "https://www.youtube.com/watch?v=example1",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 2,
                Title = "Poke Bowl Delight",
                MadeBy = "Jane Smith",
                ImageUrl = "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=400&h=400&fit=crop",
                Ingredients = "Sushi rice, salmon, avocado, edamame, seaweed, soy sauce, sesame seeds",
                Description = "A delicious Hawaiian-inspired poke bowl with fresh ingredients",
                MakingProcess = "Cook sushi rice according to package instructions. Cut salmon into cubes. Slice avocado. Cook edamame. Assemble bowl with rice as base. Add salmon, avocado, and edamame. Garnish with seaweed and sesame seeds. Drizzle with soy sauce.",
                VideoLink = "https://www.youtube.com/watch?v=example2",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 3,
                Title = "Chicken Wrap Supreme",
                MadeBy = "John Doe",
                ImageUrl = "https://images.unsplash.com/photo-1626700051175-6818013e1d4f?w=400&h=400&fit=crop",
                Ingredients = "Tortilla wraps, grilled chicken, lettuce, tomatoes, ranch dressing, cheese",
                Description = "A tasty and convenient chicken wrap perfect for lunch",
                MakingProcess = "Grill chicken breast until fully cooked. Slice chicken into strips. Warm tortilla wraps. Layer lettuce on tortilla. Add chicken strips. Add sliced tomatoes and cheese. Drizzle with ranch dressing. Roll tightly and cut in half.",
                VideoLink = "https://www.youtube.com/watch?v=example3",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 4,
                Title = "Classic Caesar Salad",
                MadeBy = "Jane Smith",
                ImageUrl = "https://images.unsplash.com/photo-1550304943-4f24f54ddde9?w=400&h=400&fit=crop",
                Ingredients = "Romaine lettuce, croutons, parmesan cheese, Caesar dressing, lemon",
                Description = "Traditional Caesar salad with a tangy dressing",
                MakingProcess = "Chop romaine lettuce. Prepare Caesar dressing. Toss lettuce with dressing. Add croutons. Shave parmesan cheese on top. Squeeze fresh lemon juice. Serve immediately.",
                VideoLink = "https://www.youtube.com/watch?v=example4",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 5,
                Title = "Mediterranean Bowl",
                MadeBy = "John Doe",
                ImageUrl = "https://images.unsplash.com/photo-1529042410759-befb1204b468?w=400&h=400&fit=crop",
                Ingredients = "Quinoa, chickpeas, feta cheese, olives, cucumber, tomatoes, olive oil",
                Description = "A nutritious Mediterranean-inspired bowl",
                MakingProcess = "Cook quinoa. Drain and rinse chickpeas. Dice cucumbers and tomatoes. Combine all ingredients in a bowl. Crumble feta cheese on top. Add olives. Drizzle with olive oil. Mix well before serving.",
                VideoLink = "https://www.youtube.com/watch?v=example5",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 6,
                Title = "Asian Fusion Salad",
                MadeBy = "Jane Smith",
                ImageUrl = "https://images.unsplash.com/photo-1505253716362-afaea1d3d1af?w=400&h=400&fit=crop",
                Ingredients = "Cabbage, carrots, edamame, peanuts, ginger dressing, cilantro",
                Description = "A crunchy Asian-inspired salad with peanut dressing",
                MakingProcess = "Shred cabbage finely. Julienne carrots. Cook edamame. Combine vegetables in bowl. Make ginger dressing. Toss salad with dressing. Top with crushed peanuts and cilantro.",
                VideoLink = "https://www.youtube.com/watch?v=example6",
                IsLiked = false,
                IsBookmarked = false
            },
            new Recipe
            {
                Id = 7,
                Title = "Greek Salad Bowl",
                MadeBy = "John Doe",
                ImageUrl = "https://images.unsplash.com/photo-1540189549336-e6e99c3679fe?w=400&h=400&fit=crop",
                Ingredients = "Mixed greens, cucumber, tomatoes, red onion, feta, olives, oregano",
                Description = "A classic Greek salad with authentic flavors",
                MakingProcess = "Chop all vegetables. Slice red onions thinly. Mix greens and vegetables. Add kalamata olives. Crumble feta cheese. Sprinkle with oregano. Dress with olive oil and lemon.",
                VideoLink = "https://www.youtube.com/watch?v=example7",
                IsLiked = false,
                IsBookmarked = false
            }
        };

        public static Recipe GetRecipeById(int id)
        {
            return Recipes.FirstOrDefault(r => r.Id == id);
        }

        public static void ToggleLike(int recipeId)
        {
            var recipe = GetRecipeById(recipeId);
            if (recipe != null)
            {
                recipe.IsLiked = !recipe.IsLiked;
            }
        }

        public static void ToggleBookmark(int recipeId)
        {
            var recipe = GetRecipeById(recipeId);
            if (recipe != null)
            {
                recipe.IsBookmarked = !recipe.IsBookmarked;
            }
        }
    }
}