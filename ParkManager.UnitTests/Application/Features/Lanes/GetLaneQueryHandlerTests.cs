using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetLaneQueryHandlerTests
    {
        private readonly Mock<ILanesRepository> _mockLanesRepository;
        private readonly IRequestHandler<GetLaneQuery, Lane> _handler;

        public GetLaneQueryHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _handler = new GetLaneQueryHandler(_mockLanesRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnLane_WhenLaneExists()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            var expectedLane = new Lane(laneId, Guid.NewGuid(), "Lane 1");
            _mockLanesRepository.Setup(repo => repo.Get(laneId)).ReturnsAsync(expectedLane);
            var query = new GetLaneQuery(laneId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLane.Id, result.Id);
            Assert.Equal(expectedLane.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenLaneDoesNotExist()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            _mockLanesRepository.Setup(repo => repo.Get(laneId)).ReturnsAsync((Lane)null);
            var query = new GetLaneQuery(laneId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
