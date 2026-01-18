using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsDto?> CreateOrderAsync(OrderDto dto);
        Task<OrderDetailsDto?> GetOrderByIdAsync(int id);
        Task<OrderDetailsDto?> GetOrderByTrackingCodeAsync(string trackingCode);
        Task<List<OrderDetailsDto>> GetUserOrdersAsync(int userId);
        Task<List<OrderDetailsDto>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}