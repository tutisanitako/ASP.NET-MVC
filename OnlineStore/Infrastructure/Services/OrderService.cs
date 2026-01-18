using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetailsDto?> CreateOrderAsync(OrderDto dto)
        {
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.SessionId == dto.SessionId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                return null;

            var orderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
            var trackingCode = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();

            var order = new Order
            {
                OrderNumber = orderNumber,
                TrackingCode = trackingCode,
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                Address = dto.Address,
                Phone = dto.Phone,
                OrderDate = DateTime.Now,
                Status = "Pending",
                UserId = dto.UserId,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id);
        }

        public async Task<OrderDetailsDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return MapToOrderDetailsDto(order);
        }

        public async Task<OrderDetailsDto?> GetOrderByTrackingCodeAsync(string trackingCode)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.TrackingCode == trackingCode);

            if (order == null) return null;

            return MapToOrderDetailsDto(order);
        }

        public async Task<List<OrderDetailsDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(MapToOrderDetailsDto).ToList();
        }

        public async Task<List<OrderDetailsDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(MapToOrderDetailsDto).ToList();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        private OrderDetailsDto MapToOrderDetailsDto(Order order)
        {
            return new OrderDetailsDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                TrackingCode = order.TrackingCode,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                Address = order.Address,
                Phone = order.Phone,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    ProductImage = oi.Product.ImageUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
    }
}