using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class SlotCapturedTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var timestamp = DateTime.Now;

            // Act
            var slotCaptured = new SlotCaptured(slotId, vehicleId, timestamp);

            // Assert
            Assert.Equal(slotId, slotCaptured.SlotId);
            Assert.Equal(vehicleId, slotCaptured.VehicleId);
            Assert.Equal(timestamp, slotCaptured.Timestamp);
            Assert.Equal(EventType.Captured, slotCaptured.EventType);
        }
    }
}

