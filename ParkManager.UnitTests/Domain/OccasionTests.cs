using Xunit;
using ParkManager.Domain;
using System;

namespace ParkManager.UnitTests
{
    public class OccasionTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act
            var occasion = new Occasion(name, startDate, endDate);

            // Assert
            Assert.Equal(name, occasion.Name);
            Assert.Equal(startDate, occasion.StartDate);
            Assert.Equal(endDate, occasion.EndDate);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            string name = null;
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Occasion(name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenStartDateIsAfterEndDate()
        {
            // Arrange
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(2);
            var endDate = DateTime.Now.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Occasion(name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenStartDateIsInThePast()
        {
            // Arrange
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Occasion(name, startDate, endDate));
        }
    }
}
