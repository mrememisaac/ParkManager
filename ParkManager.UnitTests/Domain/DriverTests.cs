using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class DriverTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var name = "Test Driver";
            var phoneNumber = "1234567890";

            // Act
            var driver = new Driver(name, phoneNumber);

            // Assert
            Assert.Equal(name, driver.Name);
            Assert.Equal(phoneNumber, driver.PhoneNumber);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            string name = null;
            var phoneNumber = "1234567890";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Driver(name, phoneNumber));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPhoneNumberIsNull()
        {
            // Arrange
            var name = "Test Driver";
            string phoneNumber = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Driver(name, phoneNumber));
        }

        [Fact]
        public void AddDriverMetadata_ShouldAddMetadataToDriverDetails()
        {
            // Arrange
            var driver = new Driver("Test Driver", "1234567890");
            var metadata = new DriverMetadata(Guid.NewGuid(), "Key", "Value");

            // Act
            driver.AddDriverMetadata(metadata);

            // Assert
            Assert.Contains(metadata, driver.DriverDetails);
        }

        [Fact]
        public void RemoveDriverMetadata_ShouldRemoveMetadataFromDriverDetails()
        {
            // Arrange
            var driver = new Driver("Test Driver", "1234567890");
            var metadata = new DriverMetadata(Guid.NewGuid(), "Key", "Value");
            driver.AddDriverMetadata(metadata);

            // Act
            driver.RemoveDriverMetadata(metadata);

            // Assert
            Assert.DoesNotContain(metadata, driver.DriverDetails);
        }

    }
}
