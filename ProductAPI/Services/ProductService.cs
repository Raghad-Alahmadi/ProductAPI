// Services/ProductService.cs
using ProductAPI.Models;
using Microsoft.Extensions.Configuration;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();
        private readonly string _defaultCurrency;
        private int _nextId = 1;

        public ProductService(IConfiguration configuration)
        {
            _defaultCurrency = configuration["DefaultCurrency"] ?? "USD";

            // Add some sample data
            _products.Add(new Product
            {
                Id = _nextId++,
                Name = "Sample Product 1",
                Description = "This is a sample product",
                Price = 19.99m,
                Currency = _defaultCurrency
            });
            _products.Add(new Product
            {
                Id = _nextId++,
                Name = "Sample Product 2",
                Description = "This is another sample product",
                Price = 29.99m,
                Currency = _defaultCurrency
            });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product? GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Currency))
            {
                product.Currency = _defaultCurrency;
            }

            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }
    }
}
