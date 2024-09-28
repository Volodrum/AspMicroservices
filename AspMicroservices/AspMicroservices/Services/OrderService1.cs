using OrderService.Interfaces;
using SharedModels;

namespace OrderService.Services
{
    public class OrderService1 : IOrderService
    {
        private static readonly List<Order> Orders = new List<Order>();
        private static int _lastOrderId = Orders.Any() ? Orders.Max(n => n.Id) : 0;

        public async Task<Order?> CreateOrder(Order order)
        {
            _lastOrderId++;
            order.Id = _lastOrderId;
            Orders.Add(order);
            return order;
        }

        public async Task<Order?> GetOrder(int id)
        {
            return Orders.FirstOrDefault(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return Orders;
        }

        public async Task<Order?> UpdateOrder(int id, Order updatedOrder)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null) return null;

            existingOrder.UserId = updatedOrder.UserId;
            existingOrder.ProductId = updatedOrder.ProductId;
            existingOrder.Quantity = updatedOrder.Quantity;

            return existingOrder;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return false;

            Orders.Remove(order);
            return true;
        }
    }
}
