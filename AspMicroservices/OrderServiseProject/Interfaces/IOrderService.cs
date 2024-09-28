using SharedModels;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Order? CreateOrder(Order order);
        Order? GetOrder(int id);
        IEnumerable<Order> GetOrders();
        Order? UpdateOrder(int id, Order updatedOrder);
        bool DeleteOrder(int id);
    }

}
