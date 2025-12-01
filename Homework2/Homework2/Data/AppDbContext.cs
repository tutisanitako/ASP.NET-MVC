using Homework2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Homework2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" },
                new Category { Id = 4, Name = "Home & Garden" },
                new Category { Id = 5, Name = "Sports" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Description = "Latest smartphone", CategoryId = 1 },
                new Product { Id = 3, Name = "Headphones", Price = 149.99m, Description = "Wireless headphones", CategoryId = 1 },
                new Product { Id = 4, Name = "T-Shirt", Price = 19.99m, Description = "Cotton t-shirt", CategoryId = 2 },
                new Product { Id = 5, Name = "Jeans", Price = 49.99m, Description = "Blue denim jeans", CategoryId = 2 },
                new Product { Id = 6, Name = "Novel Book", Price = 14.99m, Description = "Bestselling novel", CategoryId = 3 },
                new Product { Id = 7, Name = "Cookbook", Price = 24.99m, Description = "Delicious recipes", CategoryId = 3 },
                new Product { Id = 8, Name = "Garden Tools Set", Price = 79.99m, Description = "Complete garden tools", CategoryId = 4 },
                new Product { Id = 9, Name = "Basketball", Price = 29.99m, Description = "Professional basketball", CategoryId = 5 },
                new Product { Id = 10, Name = "Yoga Mat", Price = 34.99m, Description = "Non-slip yoga mat", CategoryId = 5 }
            );
        }
    }
}