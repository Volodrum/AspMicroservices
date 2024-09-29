using OrderService.Interfaces;
using SharedModels;
using System.Text.Json;

namespace OrderService.Services
{
    public class OrderService1 : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _userServiceUrl = "https://localhost:7290/api/user";
        private readonly string _productServiceUrl = "https://localhost:7299/api/product";

        public OrderService1(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User?> GetUserById(int userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_userServiceUrl}/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(json);
        }

        public async Task<Product?> GetProductById(int productId)
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
