using SharedModels;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetProductById(int productId);
    }

}
