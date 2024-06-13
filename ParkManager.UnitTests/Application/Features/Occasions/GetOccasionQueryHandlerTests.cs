using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetOccasionQueryHandlerTests
    {
        private readonly Mock<IOccasionsRepository> _mockOccasionsRepository;
        private readonly IRequestHandler<GetOccasionQuery, Occasion> _handler;

        public GetOccasionQueryHandlerTests()
        {
            _mockOccasionsRepository = new Mock<IOccasionsRepository>();
            _handler = new GetOccasionQueryHandler(_mockOccasionsRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOccasion_WhenOccasionExists()
        {
            // Arrange
            var occasionId = Guid.NewGuid();
            var expectedOccasion = new Occasion(occasionId, "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            _mockOccasionsRepository.Setup(repo => repo.Get(occasionId)).ReturnsAsync(expectedOccasion);
            var query = new GetOccasionQuery(occasionId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOccasion.Id, result.Id);
        }
    }
}
