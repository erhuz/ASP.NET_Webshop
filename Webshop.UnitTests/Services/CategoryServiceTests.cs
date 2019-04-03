using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Webshop.Repositories;
using Webshop.Services;
using Webshop.Models;
using FakeItEasy;

namespace Tests
{
    public class CategoryServiceTests

    {
        private ICategoryRepository _categoryRepository;
        private CategoryService _categoryService;
        
        [SetUp]
        public void Setup()
        {
            this._categoryRepository = A.Fake<ICategoryRepository>();
            this._categoryService = new CategoryService(this._categoryRepository);
        }

        [Test]
        public void Get_ReturnsResultFromRepository()
        {
            // Arrange
            var category = new Category()
            {
                Title = "Other",
                Description = "Non-categorised products"
            };

            var categories = new List<Category>
            {
                category
            };

            A.CallTo(() => this._categoryRepository.Get()).Returns(categories);
            // Act
            var result = this._categoryService.Get().Single();
            
            // Assert
            Assert.That(result, Is.EqualTo(category));
        }
    }
}