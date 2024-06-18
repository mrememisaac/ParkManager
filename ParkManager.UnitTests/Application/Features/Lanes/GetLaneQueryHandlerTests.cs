using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetLaneQueryHandlerTests
    {
        private readonly Mock<ILanesRepository> _mockLanesRepository;
        private readonly IRequestHandler<GetLaneQuery, GetLaneQueryResponse> _handler;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<GetLaneQueryHandler>> _mockLogger;

        public GetLaneQueryHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetLaneQueryHandler>>();
            _handler = new GetLaneQueryHandler(_mockLanesRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnLane_WhenLaneExists()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            var expectedLane = new Lane(laneId, Guid.NewGuid(), "Lane 1");
            var expectedResponse = new GetLaneQueryResponse
            {
                Id = expectedLane.Id,
                Name = expectedLane.Name,
                ParkId = expectedLane.ParkId
            };
            _mockLanesRepository.Setup(repo => repo.Get(laneId)).ReturnsAsync(expectedLane);
            _mockMapper.Setup(m => m.Map<GetLaneQueryResponse>(expectedLane)).Returns(expectedResponse);
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
