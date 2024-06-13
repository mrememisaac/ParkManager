using Xunit;
using ParkManager.Domain;
using System;
using System.Linq;

namespace ParkManager.UnitTests
{
    public class ArrivalTests
    {
        [Fact]
        public void AddImage_ShouldAddImageToImagesCollection()
        {
            // Arrange
            var arrival = new Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var image = new ArrivalImage(Guid.NewGuid(), "abc.jpg");

            // Act
            arrival.AddImage(image);

            // Assert
            Assert.Contains(image, arrival.Images);
        }

        [Fact]
        public void RemoveImage_ShouldRemoveImageFromImagesCollection()
        {
            // Arrange
            var arrival = new Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var image = new ArrivalImage(Guid.NewGuid(), "abc.jpg");
            arrival.AddImage(image);

            // Act
            arrival.RemoveImage(image);

            // Assert
            Assert.DoesNotContain(image, arrival.Images);
        }

        [Fact]
        public void Constructor_ShouldInitializeImagesCollection()
        {
            // Arrange
            var arrival = new Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.NotNull(arrival.Images);
            Assert.Empty(arrival.Images);
        }
    }
}
