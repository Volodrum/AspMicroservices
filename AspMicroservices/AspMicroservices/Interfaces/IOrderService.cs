using SharedModels;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateOrder(Order order);
        Task<Order?> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order?> UpdateOrder(int id, Order updatedOrder);
        Task<bool> DeleteOrder(int id);
    }
}
