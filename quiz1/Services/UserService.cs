using quiz1.Models;

namespace quiz1.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Email = "user@example.com",
                Password = "TempPass123",
                IsFirstLogin = true
            },
            new User
            {
                Id = 2,
                Email = "test@test.com",
                Password = "TestPass123",
                IsFirstLogin = true
            },
            new User
            {
                Id = 3,
                Email = "admin@example.com",
                Password = "AdminPass123",
                IsFirstLogin = true
            }
        };

        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public void AddUser(User user)
        {
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                existingUser.IsFirstLogin = user.IsFirstLogin;
            }
        }

        public bool ValidateUser(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && user.Password == password;
        }
    }
}