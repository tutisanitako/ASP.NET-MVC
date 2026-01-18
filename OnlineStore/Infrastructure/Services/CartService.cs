using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(string sessionId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.SessionId == sessionId)
                .Select(c => new CartItemDto
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Price = c.Product.Price,
                    Quantity = c.Quantity,
                    ImageUrl = c.Product.ImageUrl
                }).ToListAsync();
        }

        public async Task<CartItemDto?> AddToCartAsync(AddToCartDto dto)
        {
            var existingItem = await _context.CartItems
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.SessionId == dto.SessionId && c.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                await _context.SaveChangesAsync();

                return new CartItemDto
                {
                    Id = existingItem.Id,
                    ProductId = existingItem.ProductId,
                    ProductName = existingItem.Product.Name,
                    Price = existingItem.Product.Price,
                    Quantity = existingItem.Quantity,
                    ImageUrl = existingItem.Product.ImageUrl
                };
            }

            var product = await _context.Products.FindAsync(dto.ProductId);

            // Fix: Check if product is null
            if (product == null)
                return null;

            var cartItem = new CartItem
            {
                SessionId = dto.SessionId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                AddedAt = DateTime.Now
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = cartItem.Quantity,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<bool> UpdateCartItemAsync(int id, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) return false;

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}