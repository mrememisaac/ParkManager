using Xunit;
using ParkManager.Domain;
using ParkManager.Domain.Exceptions;
using System;

namespace ParkManager.UnitTests
{
    public class DepartureImageTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            var path = "test/path";

            // Act
            var departureImage = new DepartureImage(departureId, path);

            // Assert
            Assert.Equal(departureId, departureImage.DepartureId);
            Assert.Equal(path, departureImage.Path);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDepartureIdIsEmpty()
        {
            // Arrange
            var departureId = Guid.Empty;
            var path = "test/path";

            // Act & Assert
            Assert.Throws<EmptyGuidException>(() => new DepartureImage(departureId, path));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPathIsNull()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            string path = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DepartureImage(departureId, path));
        }
    }
}
