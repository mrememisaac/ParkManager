using Xunit;
using ParkManager.Domain;
using System;
using System.Linq;

namespace ParkManager.UnitTests
{
    public class DepartureTests
    {
        [Fact]
        public void AddImage_ShouldAddImageToImagesCollection()
        {
            // Arrange
            var departure = new Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var image = new DepartureImage(Guid.NewGuid(), "abc.jpg");

            // Act
            departure.AddImage(image);

            // Assert
            Assert.Contains(image, departure.Images);
        }

        [Fact]
        public void RemoveImage_ShouldRemoveImageFromImagesCollection()
        {
            // Arrange
            var departure = new Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var image = new DepartureImage(Guid.NewGuid(), "abc.jpg");
            departure.AddImage(image);

            // Act
            departure.RemoveImage(image);

            // Assert
            Assert.DoesNotContain(image, departure.Images);
        }

        [Fact]
        public void Constructor_ShouldInitializeImagesCollection()
        {
            // Arrange
            var departure = new Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.NotNull(departure.Images);
            Assert.Empty(departure.Images);
        }
    }
}
