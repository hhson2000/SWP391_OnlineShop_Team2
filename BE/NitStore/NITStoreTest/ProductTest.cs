﻿using Microsoft.EntityFrameworkCore;
using NitStore.Controllers;
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
    public class ProductTest
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
        public void CreateProduct_AddsNewProductToDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var product = new Product
            {
                Id = 1,
                Name = "Test Category",
                Category = 1,
                Status = 1,
                Money = 1,
                Quantity = 1,
                Description = "This is a test category"
            };

            // Act
            context.Product.Add(product);
            context.SaveChanges();

            // Assert
            var savedCategory = context.Product.FirstOrDefault(c => c.Id == product.Id);
            Assert.NotNull(savedCategory);
            Assert.AreEqual(product.Name, savedCategory.Name);
            Assert.AreEqual(product.Description, savedCategory.Description);
        }

        [Test]
        public void CreateCategoryFail_AddsNewCategoryToDatabaseFailed()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var product = new Product
            {
                Name = "",
                Category = 1,
                Status = 1,
                Money = 1,
                Quantity = 1,
                Description = "This is a test category"
            };

            // Act
            ProductController repo = new ProductController(context);
            var resultTask = repo.AddProduct(product);
            resultTask.Wait();
            bool result = resultTask.Result;
            // Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void ReadProduct_GetsProductFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var product = new Product
            {
                Id = 1,
                Name = "Test Category",
                Category = 1,
                Status = 1,
                Money = 1,
                Quantity = 1,
                Description = "This is a test category"
            };
            context.Product.Add(product);
            context.SaveChanges();

            // Act
            var savedCategory = context.Product.FirstOrDefault(c => c.Id == product.Id);

            // Assert
            Assert.NotNull(savedCategory);
            Assert.AreEqual(product.Name, savedCategory.Name);
            Assert.AreEqual(product.Description, savedCategory.Description);
        }

        [Test]
        public void UpdateProduct_UpdatesProductInDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var product = new Product
            {
                Id = 1,
                Name = "Test Category",
                Category = 1,
                Status = 1,
                Money = 1,
                Quantity = 1,
                Description = "This is a test category"
            }; ;
            context.Product.Add(product);
            context.SaveChanges();

            // Act
            var savedCategory = context.Product.FirstOrDefault(c => c.Id == product.Id);
            savedCategory.Name = "Updated Test Category";
            savedCategory.Description = "This is an updated test category";
            context.SaveChanges();

            // Assert
            var updatedProduct = context.Product.FirstOrDefault(c => c.Id == product.Id);
            Assert.NotNull(updatedProduct);
            Assert.AreEqual(savedCategory.Name, updatedProduct.Name);
            Assert.AreEqual(savedCategory.Description, updatedProduct.Description);
        }

        [Test]
        public void DeleteProduct_RemovesProductFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var product = new Product
            {
                Id = 1,
                Name = "Test Category",
                Category = 1,
                Status = 1,
                Money = 1,
                Quantity = 1,
                Description = "This is a test category"
            };
            context.Product.Add(product);
            context.SaveChanges();

            // Act
            context.Product.Remove(product);
            context.SaveChanges();

            // Assert
            var deletedProduct = context.Product.FirstOrDefault(c => c.Id == product.Id);
            Assert.Null(deletedProduct);
        }
    }
}
