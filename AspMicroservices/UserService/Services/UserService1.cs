using SharedModels;
using UserService.Interfaces;

namespace UserService.Services
{
    public class UserService1 : IUserService
    {
        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        public async Task<User?> GetUserById(int userId)
        {
            return Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
