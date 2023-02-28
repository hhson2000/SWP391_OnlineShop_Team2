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
    public class ImageTest
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
        public void CreateImage_AddsNewImageToDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var image = new Image
            {
                ImageURL = "Test  url",
                Description = "This is a test image"
            };

            // Act
            context.images.Add(image);
            context.SaveChanges();

            // Assert
            var savedImage = context.images.FirstOrDefault(c => c.Id == image.Id);
            Assert.NotNull(savedImage);
            Assert.AreEqual(image.ImageURL, savedImage.ImageURL);
            Assert.AreEqual(image.Description, savedImage.Description);
        }

        [Test]
        public void ReadImage_GetsImageFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var image = new Image
            {
                ImageURL = "Test  url",
                Description = "This is a test image"
            };
            context.images.Add(image);
            context.SaveChanges();

            // Act
            var savedImage = context.images.FirstOrDefault(c => c.Id == image.Id);

            // Assert
            Assert.NotNull(savedImage);
            Assert.AreEqual(image.ImageURL, savedImage.ImageURL);
            Assert.AreEqual(image.Description, savedImage.Description);
        }

        [Test]
        public void UpdateImage_UpdatesImageInDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var image = new Image
            {
                ImageURL = "Test  url",
                Description = "This is a test image"
            };
            context.images.Add(image);
            context.SaveChanges();

            // Act
            var savedImage = context.images.FirstOrDefault(c => c.Id == image.Id);
            savedImage.ImageURL = "Test URL";
            savedImage.Description = "This is an updated test category";
            context.SaveChanges();

            // Assert
            var updatedImage = context.images.FirstOrDefault(c => c.Id == image.Id);
            Assert.NotNull(updatedImage);
            Assert.AreEqual(savedImage.ImageURL, updatedImage.ImageURL);
            Assert.AreEqual(savedImage.Description, updatedImage.Description);
        }

        [Test]
        public void DeleteImage_RemovesImageFromDatabase()
        {
            // Arrange
            using var context = new NitDbContext(_options);
            var image = new Image
            {
                ImageURL = "Test  url",
                Description = "This is a test image"
            };
            context.images.Add(image);
            context.SaveChanges();

            // Act
            context.images.Remove(image);
            context.SaveChanges();

            // Assert
            var deletedImage = context.images.FirstOrDefault(c => c.Id == image.Id);
            Assert.Null(deletedImage);
        }
    }
}

