using Microsoft.EntityFrameworkCore;
using Moq;
using ParkManager.Application.Contracts.Authentication;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests.Persistence
{
    public class ParkMangerDbContextTests
    {
        private readonly ParkManagerDbContext _dbContext;
        private readonly Mock<ILoggedInUserService> _mockLoggedInUserService;

        public ParkMangerDbContextTests()
        {
            var options = new DbContextOptionsBuilder<ParkManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "ParkManagerTestDb")
                .Options;
            _mockLoggedInUserService = new Mock<ILoggedInUserService>();
            _dbContext = new ParkManagerDbContext(options, _mockLoggedInUserService.Object);
        }

        [Fact]
        public async Task SaveChangesAsync_SetsCreatedFields_OnEntityAdded()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockLoggedInUserService.Setup(s => s.UserId).Returns(userId);
            Park park = CreateDefaultPark();
            _dbContext.Parks.Add(park);

            // Act
            await _dbContext.SaveChangesAsync();

            // Assert
            Assert.Equal(userId, park.CreatedBy);
            Assert.True(park.CreatedDate <= DateTime.Now);
        }

        private static Park CreateDefaultPark()
        {
            return new Park(Guid.NewGuid(), "Test Park", "Test Street", "Test City", "Test State", "Test Country", 1.1, 1.1);
        }

        [Fact]
        public async Task SaveChangesAsync_SetsModifiedFields_OnEntityModified()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockLoggedInUserService.Setup(s => s.UserId).Returns(userId);
            var park = CreateDefaultPark();
            _dbContext.Parks.Add(park);
            await _dbContext.SaveChangesAsync(); // Save once to add the entity

            // Modify the entity
            var newVersion = new Park(park.Id, "Updated Test Park", park.Street, park.City, park.State, park.Country, park.Latitude, park.Longitude);
            park.Update(newVersion);
            _dbContext.Parks.Update(park);

            // Act
            await _dbContext.SaveChangesAsync();

            // Assert
            Assert.Equal(userId, park.LastModifiedBy);
            Assert.True(park.LastModifiedDate <= DateTime.Now);
        }
    }
}
