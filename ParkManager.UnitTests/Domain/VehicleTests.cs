using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class VehicleTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var make = "Test Make";
            var model = "Test Model";
            var registration = "Test Registration";

            // Act
            var vehicle = new Vehicle(id,make, model, registration);

            // Assert
            Assert.Equal(make, vehicle.Make);
            Assert.Equal(model, vehicle.Model);
            Assert.Equal(registration, vehicle.Registration);
        }

        [Fact]
        public void AddImage_ShouldAddImageToImagesCollection()
        {
            // Arrange
            var id = Guid.NewGuid();
            var vehicle = new Vehicle(id, "Test Make", "Test Model", "Test Registration");
            var image = new VehicleImage(Guid.NewGuid(), "test/path");

            // Act
            vehicle.AddImage(image);

            // Assert
            Assert.Contains(image, vehicle.Images);
        }

        [Fact]
        public void RemoveImage_ShouldRemoveImageFromImagesCollection()
        {
            // Arrange
            var id = Guid.NewGuid();
            var vehicle = new Vehicle(id, "Test Make", "Test Model", "Test Registration");
            var image = new VehicleImage(Guid.NewGuid(), "test/path");
            vehicle.AddImage(image);

            // Act
            var result = vehicle.RemoveImage(image);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(image, vehicle.Images);
        }
    }
}

