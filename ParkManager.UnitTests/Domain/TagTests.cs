using Xunit;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class TagTests
    {

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var number = 123;
            var id = System.Guid.NewGuid();

            // Act
            var tag = new Tag(id, number);

            // Assert
            Assert.Equal(number, tag.Number);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNumberIsNegative()
        {
            // Arrange
            var id = System.Guid.NewGuid();
            var number = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Tag(id, number));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenIdIsEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var number = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Tag(id, number));
        }
    }
}

