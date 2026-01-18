using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Stock = p.Stock,
                    Category = p.Category
                }).ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                Category = product.Category
            };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock,
                Category = dto.Category,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                Category = product.Category
            };
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.Stock = dto.Stock;
            product.Category = dto.Category;

            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                Category = product.Category
            };
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}