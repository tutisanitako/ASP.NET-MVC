using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCartItemsAsync(string sessionId);
        Task<CartItemDto?> AddToCartAsync(AddToCartDto dto);
        Task<bool> UpdateCartItemAsync(int id, int quantity);
        Task<bool> RemoveFromCartAsync(int id);
    }
}