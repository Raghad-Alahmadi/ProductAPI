using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;
using System.Collections.Generic;
using Xunit;

namespace ProductAPI.Test
{
    public class ProductsControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ProductsController _controller;

        public ProductsControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(c => c["AppName"]).Returns("TestApp");
            _mockConfiguration.Setup(c => c["DefaultCurrency"]).Returns("USD");

            _controller = new ProductsController(
                _mockProductService.Object,
                _mockLogger.Object,
                _mockConfiguration.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10, Currency = "USD" },
                new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 20, Currency = "USD" }
            };
            _mockProductService.Setup(s => s.GetAllProducts()).Returns(products);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10, Currency = "USD" };
            _mockProductService.Setup(s => s.GetProduct(1)).Returns(product);

            // Act
            var result = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, returnedProduct.Id);
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProduct(1)).Returns((Product)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtActionResult_WithNewProduct()
        {
            // Arrange
            var product = new Product { Name = "Product1", Description = "Description1", Price = 10, Currency = "USD" };
            var createdProduct = new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10, Currency = "USD" };
            _mockProductService.Setup(s => s.AddProduct(product)).Returns(createdProduct);

            // Act
            var result = _controller.Create(product);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(createdAtActionResult.Value);
            Assert.Equal(1, returnedProduct.Id);
        }


    }
}
