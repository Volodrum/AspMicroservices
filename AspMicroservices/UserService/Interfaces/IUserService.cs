using SharedModels;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int userId);
    }

}
