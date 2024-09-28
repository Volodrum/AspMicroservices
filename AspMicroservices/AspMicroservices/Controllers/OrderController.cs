using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;
using ProductService.Interfaces;
using SharedModels;
using UserService.Interfaces;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IUserService userService, IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var user = await _userService.GetUserById(order.UserId);
            if (user == null)
                return BadRequest("User not found");

            var product = await _productService.GetProductById(order.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            var createdOrder = await _orderService.CreateOrder(order);
            return Ok(createdOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var user = await _userService.GetUserById(updatedOrder.UserId);
            if (user == null)
                return BadRequest("User not found");

            var product = await _productService.GetProductById(updatedOrder.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            var order = await _orderService.UpdateOrder(id, updatedOrder);
            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result)
                return NotFound("Order not found");

            return NoContent();
        }
    }
}
