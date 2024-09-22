using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _userServiceUrl = "https://localhost:7290/api/user";
        private readonly string _productServiceUrl = "https://localhost:7299/api/product";

        private static readonly List<Order> Orders = new List<Order>();
        private static int _lastOrderId = Orders.Any() ? Orders.Max(n => n.Id) : 0;
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var user = await GetUserById(order.UserId);
            if (user == null)
                return BadRequest("User not found");

            var product = await GetProductById(order.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            _lastOrderId++;
            order.Id = _lastOrderId;

            Orders.Add(order);
            return Ok(order);
        }

        private async Task<User?> GetUserById(int userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_userServiceUrl}/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(json);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetOrder(int id)
        {
            var user = Orders.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetOrders()
        {
            return Ok(Orders);
        }

        private async Task<Product?> GetProductById(int productId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_productServiceUrl}/{productId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(json);
        }
    }
}
