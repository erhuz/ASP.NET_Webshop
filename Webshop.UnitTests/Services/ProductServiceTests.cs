using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Webshop.Repositories;
using Webshop.Services;
using Webshop.Models;
using FakeItEasy;

namespace Tests
{
    public class ProductServiceTests
    {
        private IProductRepository _productRepository;
        private ProductService _productService;
        
        [SetUp]
        public void Setup()
        {
            this._productRepository = A.Fake<IProductRepository>();
            this._productService = new ProductService(this._productRepository);
        }

        [Test]
        public void Get_ReturnsResultFromRepository()
        {
            // Arrange
            var product = new Product
            {
                Price = 899,
                CategoryId = 5,
                Title = "Mixer",
                Description = "It will blend anything"
            };

            var products = new List<Product>
            {
                product
            };

            A.CallTo(() => this._productRepository.Get()).Returns(products);
            // Act
            var result = this._productService.Get().Single();
            
            // Assert
            Assert.That(result, Is.EqualTo(product));
        }
    }
}