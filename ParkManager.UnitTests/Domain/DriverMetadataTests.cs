using Xunit;
using ParkManager.Domain;
using ParkManager.Domain.Exceptions;
using System;

namespace ParkManager.UnitTests
{
    public class DriverMetadataTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var key = "testKey";
            var value = "testValue";

            // Act
            var driverMetadata = new DriverMetadata(driverId, key, value);

            // Assert
            Assert.Equal(driverId, driverMetadata.DriverId);
            Assert.Equal(key, driverMetadata.Key);
            Assert.Equal(value, driverMetadata.Value);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDriverIdIsEmpty()
        {
            // Arrange
            var driverId = Guid.Empty;
            var key = "testKey";
            var value = "testValue";

            // Act & Assert
            Assert.Throws<EmptyGuidException>(() => new DriverMetadata(driverId, key, value));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenKeyIsNull()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            string key = null;
            var value = "testValue";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverMetadata(driverId, key, value));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenValueIsNull()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var key = "testKey";
            string value = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DriverMetadata(driverId, key, value));
        }
    }
}
