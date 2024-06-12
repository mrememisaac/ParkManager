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
            var parkId = Guid.NewGuid();
            var path = "test/path";

            // Act
            var vehicleImage = new VehicleImage(parkId, path);

            // Assert
            Assert.Equal(parkId, vehicleImage.ParkId);
            Assert.Equal(path, vehicleImage.Path);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenParkIdIsEmpty()
        {
            // Arrange
            var parkId = Guid.Empty;
            var path = "test/path";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleImage(parkId, path));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPathIsNull()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            string path = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new VehicleImage(parkId, path));
        }
    }
}

