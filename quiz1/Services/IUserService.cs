using quiz1.Models;

namespace quiz1.Services
{
    public interface IUserService
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        bool ValidateUser(string email, string password);
    }
}