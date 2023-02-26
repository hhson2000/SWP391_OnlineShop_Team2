using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace NITStoreTest
{
    [TestFixture]
    public class CategoryTest
    {
        private DbContextOptions<NitDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<NitDbContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;
        }

        [Test]
        public void CreateCategory_AddsNewCategoryToDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var category = new Category
            {
                Name = "Test Category",
                Description = "This is a test category"
            };

            // Act
            context.Category.Add(category);
            context.SaveChanges();

            // Assert
            var savedCategory = context.Category.FirstOrDefault(c => c.Id == category.Id);
            Assert.NotNull(savedCategory);
            Assert.AreEqual(category.Name, savedCategory.Name);
            Assert.AreEqual(category.Description, savedCategory.Description);
        }

        [Test]
        public void ReadCategory_GetsCategoryFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var category = new Category
            {
                Name = "Test Category",
                Description = "This is a test category"
            };
            context.Category.Add(category);
            context.SaveChanges();

            // Act
            var savedCategory = context.Category.FirstOrDefault(c => c.Id == category.Id);

            // Assert
            Assert.NotNull(savedCategory);
            Assert.AreEqual(category.Name, savedCategory.Name);
            Assert.AreEqual(category.Description, savedCategory.Description);
        }

        [Test]
        public void UpdateCategory_UpdatesCategoryInDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var category = new Category
            {
                Name = "Test Category",
                Description = "This is a test category"
            };
            context.Category.Add(category);
            context.SaveChanges();

            // Act
            var savedCategory = context.Category.FirstOrDefault(c => c.Id == category.Id);
            savedCategory.Name = "Updated Test Category";
            savedCategory.Description = "This is an updated test category";
            context.SaveChanges();

            // Assert
            var updatedCategory = context.Category.FirstOrDefault(c => c.Id == category.Id);
            Assert.NotNull(updatedCategory);
            Assert.AreEqual(savedCategory.Name, updatedCategory.Name);
            Assert.AreEqual(savedCategory.Description, updatedCategory.Description);
        }

        [Test]
        public void DeleteCategory_RemovesCategoryFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var category = new Category
            {
                Name = "Test Category",
                Description = "This is a test category"
            };
            context.Category.Add(category);
            context.SaveChanges();

            // Act
            context.Category.Remove(category);
            context.SaveChanges();

            // Assert
            var deletedCategory = context.Category.FirstOrDefault(c => c.Id == category.Id);
            Assert.Null(deletedCategory);
        }
    }
}
