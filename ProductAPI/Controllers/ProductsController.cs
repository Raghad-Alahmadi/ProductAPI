// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IConfiguration _configuration;

        public ProductsController(
            IProductService productService,
            ILogger<ProductsController> logger,
            IConfiguration configuration)
        {
            _productService = productService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            _logger.LogInformation("Getting all products for {AppName}", _configuration["AppName"]);
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            var newProduct = _productService.AddProduct(product);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }
        [HttpGet("config")]
        public ActionResult GetConfig()
        {
            var config = new
            {
                AppName = _configuration["AppName"],
                DefaultCurrency = _configuration["DefaultCurrency"]
            };

            return Ok(config);
        }

    }
}
