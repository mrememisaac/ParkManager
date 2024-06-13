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
            var id = Guid.NewGuid();
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act
            var occasion = new Occasion(id,name, startDate, endDate);

            // Assert
            Assert.Equal(name, occasion.Name);
            Assert.Equal(startDate, occasion.StartDate);
            Assert.Equal(endDate, occasion.EndDate);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Occasion(id, name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenStartDateIsAfterEndDate()
        {
            // Arrange
            var id = Guid.NewGuid(); 
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(2);
            var endDate = DateTime.Now.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Occasion(id,name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenStartDateIsInThePast()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Occasion(id, name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenIdIsEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Occasion 1";
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Occasion(id, name, startDate, endDate));
        }

        [Fact]
        public void Constructor_ShouldInitializeId_WhenIdIsProvided()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Occasion 2";
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act
            var occasion = new Occasion(id, name, startDate, endDate);

            // Assert
            Assert.Equal(id, occasion.Id);
        }

    }
}
