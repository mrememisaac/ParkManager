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

            // Act
            var tag = new Tag(number);

            // Assert
            Assert.Equal(number, tag.Number);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNumberIsNegative()
        {
            // Arrange
            var number = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Tag(number));
        }
    }
}

