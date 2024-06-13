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
            var id = Guid.NewGuid();
            var laneId = Guid.NewGuid();
            var name = "Slot 1";

            // Act
            var slot = new Slot(id,laneId, name);

            // Assert
            Assert.Equal(laneId, slot.LaneId);
            Assert.Equal(name, slot.Name);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenLaneIdIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var laneId = Guid.Empty;
            var name = "Slot 1";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Slot(id,laneId, name));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var laneId = Guid.NewGuid();
            string name = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Slot(id,laneId, name));
        }
    }
}
