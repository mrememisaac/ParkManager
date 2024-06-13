using Xunit;
using ParkManager.Domain;
using System;
using System.Linq;

namespace ParkManager.UnitTests
{
    public class ParkTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Park 1";
            var street = "Street 1";
            var city = "City 1";
            var state = "State 1";
            var country = "Country 1";
            long latitude = 123456789;
            long longitude = 987654321;

            // Act
            var park = new Park(id, name, street, city, state, country, latitude, longitude);

            // Assert
            Assert.Equal(name, park.Name);
            Assert.Equal(street, park.Street);
            Assert.Equal(city, park.City);
            Assert.Equal(state, park.State);
            Assert.Equal(country, park.Country);
            Assert.Equal(latitude, park.Latitude);
            Assert.Equal(longitude, park.Longitude);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var street = "Street 1";
            var city = "City 1";
            var state = "State 1";
            var country = "Country 1";
            long latitude = 123456789;
            long longitude = 987654321;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Park(id, name, street, city, state, country, latitude, longitude));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenStreetIsNull()
        {
            // Arrange
            var id = Guid.NewGuid(); 
            var name = "Park 1";
            string street = null;
            var city = "City 1";
            var state = "State 1";
            var country = "Country 1";
            long latitude = 123456789;
            long longitude = 987654321;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Park(id, name, street, city, state, country, latitude, longitude));
        }

        // Add similar tests for City, State, and Country

        [Fact]
        public void Images_ShouldReturnReadOnlyCollection()
        {
            // Arrange
            var id = Guid.NewGuid(); 
            var name = "Park 1";
            var street = "Street 1";
            var city = "City 1";
            var state = "State 1";
            var country = "Country 1";
            long latitude = 123456789;
            long longitude = 987654321;
            var park = new Park(id, name, street, city, state, country, latitude, longitude);

            // Act
            var images = park.Images;

            // Assert
            Assert.Empty(images);
        }

        [Fact]
        public void AddLane_ShouldAddLaneToLanesCollection()
        {
            // Arrange
            var park = new Park(Guid.NewGuid(), "Park 1", "Street 1", "City 1", "State 1", "Country 1", 123456789, 987654321);
            var lane = new Lane(Guid.NewGuid(), Guid.NewGuid(), "Lane 1");

            // Act
            park.AddLane(lane);

            // Assert
            Assert.Contains(lane, park.Lanes);
        }

        [Fact]
        public void RemoveLane_ShouldRemoveLaneFromLanesCollection()
        {
            // Arrange
            var park = new Park(Guid.NewGuid(), "Park 1", "Street 1", "City 1", "State 1", "Country 1", 123456789, 987654321);
            var lane = new Lane(Guid.NewGuid(), Guid.NewGuid(), "Lane 1");
            park.AddLane(lane);

            // Act
            park.RemoveLane(lane);

            // Assert
            Assert.DoesNotContain(lane, park.Lanes);
        }

        [Fact]
        public void AddImage_ShouldAddImageToImagesCollection()
        {
            // Arrange
            var park = new Park(Guid.NewGuid(), "Test Park", "Test Street", "Test City", "Test State", "Test Country", 0, 0);
            var image = new ParkImage(Guid.NewGuid(), "Test Image");

            // Act
            park.AddImage(image);

            // Assert
            Assert.Contains(image, park.Images);
        }

        [Fact]
        public void RemoveImage_ShouldRemoveImageFromImagesCollection()
        {
            // Arrange
            var park = new Park(Guid.NewGuid(), "Test Park", "Test Street", "Test City", "Test State", "Test Country", 0, 0);
            var image = new ParkImage(Guid.NewGuid(), "Test Image");
            park.AddImage(image);

            // Act
            park.RemoveImage(image);

            // Assert
            Assert.DoesNotContain(image, park.Images);
        }
    }
}
