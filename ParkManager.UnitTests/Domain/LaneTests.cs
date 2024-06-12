using Xunit;
using ParkManager.Domain;
using ParkManager.Domain.Exceptions;
using System;

namespace ParkManager.UnitTests
{
    public class LaneTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            var name = "Lane 1";

            // Act
            var lane = new Lane(parkId, name);

            // Assert
            Assert.Equal(parkId, lane.ParkId);
            Assert.Equal(name, lane.Name);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenParkIdIsEmpty()
        {
            // Arrange
            var parkId = Guid.Empty;
            var name = "Lane 1";

            // Act & Assert
            Assert.Throws<EmptyGuidException>(() => new Lane(parkId, name));
        }
    }
}
