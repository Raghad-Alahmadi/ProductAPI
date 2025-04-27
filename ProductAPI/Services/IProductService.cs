using ProductAPI.Models;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProduct(int id);
        Product AddProduct(Product product);
    }
}
