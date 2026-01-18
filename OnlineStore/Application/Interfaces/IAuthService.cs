using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto?> RegisterAsync(RegisterDto dto);
        Task<UserDto?> LoginAsync(LoginDto dto);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}