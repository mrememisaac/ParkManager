using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class VehicleImageTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var path = "test/path";

            // Act
            var vehicleImage = new VehicleImage(vehicleId, path);

            // Assert
            Assert.Equal(vehicleId, vehicleImage.VehicleId);
            Assert.Equal(path, vehicleImage.Path);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenParkIdIsEmpty()
        {
            // Arrange
            var vehicleId = Guid.Empty;
            var path = "test/path";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleImage(vehicleId, path));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPathIsNull()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            string path = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new VehicleImage(vehicleId, path));
        }
    }
}

