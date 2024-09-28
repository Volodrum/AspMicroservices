using ProductService.Interfaces;
using SharedModels;

namespace ProductService.Services
{
    public class ProductService1 : IProductService
    {
        private static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1500 },
        new Product { Id = 2, Name = "Phone", Price = 800 }
    };

        public async Task<Product?> GetProductById(int productId)
        {
            return Products.FirstOrDefault(p => p.Id == productId);
        }
    }

}
