using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class SlotTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            var name = "Slot 1";

            // Act
            var slot = new Slot(laneId, name);

            // Assert
            Assert.Equal(laneId, slot.LaneId);
            Assert.Equal(name, slot.Name);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenLaneIdIsEmpty()
        {
            // Arrange
            var laneId = Guid.Empty;
            var name = "Slot 1";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Slot(laneId, name));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            string name = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Slot(laneId, name));
        }
    }
}
