using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> RegisterAsync(RegisterDto dto)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return null;

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsAdmin = false,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    IsAdmin = u.IsAdmin,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsAdmin) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}