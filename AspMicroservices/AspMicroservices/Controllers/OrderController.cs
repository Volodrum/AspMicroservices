using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;
using SharedModels;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1, ProductId = 1, Quantity = 1 },
            new Order { Id = 2, UserId = 2, ProductId = 2, Quantity = 2 }
        };

        private static int _lastOrderId = Orders.Any() ? Orders.Max(n => n.Id) : 0;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var user = await _orderService.GetUserById(order.UserId);
            if (user == null)
                return BadRequest("User not found");

            var product = await _orderService.GetProductById(order.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            _lastOrderId++;
            order.Id = _lastOrderId;
            Orders.Add(order);

            return Ok(order);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(Orders);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
                return NotFound("Order not found");

            var user = await _orderService.GetUserById(updatedOrder.UserId);
            if (user == null)
                return BadRequest("User not found");

            var product = await _orderService.GetProductById(updatedOrder.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            existingOrder.UserId = updatedOrder.UserId;
            existingOrder.ProductId = updatedOrder.ProductId;
            existingOrder.Quantity = updatedOrder.Quantity;

            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Order not found");

            Orders.Remove(order);
            return NoContent();
        }
    }
}
