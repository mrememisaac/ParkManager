using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class SlotReleasedTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var timestamp = DateTime.Now;

            // Act
            var slotReleased = new SlotReleased(slotId, vehicleId, timestamp);

            // Assert
            Assert.Equal(slotId, slotReleased.SlotId);
            Assert.Equal(vehicleId, slotReleased.VehicleId);
            Assert.Equal(timestamp, slotReleased.Timestamp);
            Assert.Equal(EventType.Released, slotReleased.EventType);
        }
    }
}
