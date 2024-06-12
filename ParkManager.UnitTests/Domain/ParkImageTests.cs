using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class ParkImageTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            var path = "test/path";

            // Act
            var parkImage = new ParkImage(parkId, path);

            // Assert
            Assert.Equal(parkId, parkImage.ParkId);
            Assert.Equal(path, parkImage.Path);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenParkIdIsEmpty()
        {
            // Arrange
            var parkId = Guid.Empty;
            var path = "test/path";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ParkImage(parkId, path));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPathIsNull()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            string path = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ParkImage(parkId, path));
        }
    }
}
