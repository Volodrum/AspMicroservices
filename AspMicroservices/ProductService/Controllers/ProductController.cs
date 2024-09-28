using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500 },
            new Product { Id = 2, Name = "Phone", Price = 800 }
        };

        private static int _lastOrderId = Products.Any() ? Products.Max(n => n.Id) : 0;

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(Products);
        }

        [HttpPost]
        public ActionResult<Product> CreateUser([FromBody] Product newProduct)
        {
            _lastOrderId++;
            newProduct.Id = _lastOrderId;

            Products.Add(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(u => u.Id == id);
            if (product == null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            Products.Remove(product);
            return NoContent();
        }
    }
}
