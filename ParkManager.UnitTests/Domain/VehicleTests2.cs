using ParkManager.Domain;
using System;
using Xunit;

namespace ParkManager.UnitTests.Domain
{
    public class VehicleTests2
    {
        [Fact]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var make = "Toyota";
            var model = "Corolla";
            var registration = "ABC123";

            // Act
            var vehicle = new Vehicle(id, make, model, registration);

            // Assert
            Assert.Equal(id, vehicle.Id);
            Assert.Equal(make, vehicle.Make);
            Assert.Equal(model, vehicle.Model);
            Assert.Equal(registration, vehicle.Registration);
        }

        [Fact]
        public void AddImage_WithValidImage_AddsImageToCollection()
        {
            // Arrange
            var vehicle = CreateDefaultVehicle();
            var image = new VehicleImage(Guid.NewGuid(), "image.jpg");

            // Act
            vehicle.AddImage(image);

            // Assert
            Assert.Contains(image, vehicle.Images);
        }

        [Fact]
        public void AddImage_WithNullImage_ThrowsArgumentNullException()
        {
            // Arrange
            var vehicle = CreateDefaultVehicle();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => vehicle.AddImage(null));
        }

        [Fact]
        public void AddImage_WithDuplicateImage_ThrowsArgumentException()
        {
            // Arrange
            var vehicle = CreateDefaultVehicle();
            var image = new VehicleImage(Guid.NewGuid(), "image.jpg");
            vehicle.AddImage(image);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => vehicle.AddImage(image));
        }

        [Fact]
        public void RemoveImage_WithExistingImage_RemovesImageFromCollection()
        {
            // Arrange
            var vehicle = CreateDefaultVehicle();
            var image = new VehicleImage(Guid.NewGuid(), "image.jpg");
            vehicle.AddImage(image);

            // Act
            var result = vehicle.RemoveImage(image);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(image, vehicle.Images);
        }

        [Fact]
        public void RemoveImage_WithNonExistingImage_ReturnsFalse()
        {
            // Arrange
            var vehicle = CreateDefaultVehicle();
            var image = new VehicleImage(Guid.NewGuid(), "image.jpg");

            // Act
            var result = vehicle.RemoveImage(image);

            // Assert
            Assert.False(result);
        }

        private Vehicle CreateDefaultVehicle()
        {
            return new Vehicle(Guid.NewGuid(), "DefaultMake", "DefaultModel", "DefaultRegistration");
        }
    }
}
