using SharedModels;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<User?> GetUserById(int userId);
        Task<Product?> GetProductById(int productId);
    }
}