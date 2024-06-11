using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class ArrivalImageTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            var path = "test/path";

            // Act
            var arrivalImage = new ArrivalImage(arrivalId, path);

            // Assert
            Assert.Equal(arrivalId, arrivalImage.ArrivalId);
            Assert.Equal(path, arrivalImage.Path);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenArrivalIdIsEmpty()
        {
            // Arrange
            var arrivalId = Guid.Empty;
            var path = "test/path";

            // Act & Assert
            Assert.Throws<EmptyGuidException>(() => new ArrivalImage(arrivalId, path));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPathIsNull()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            string path = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArrivalImage(arrivalId, path));
        }
    }
}
