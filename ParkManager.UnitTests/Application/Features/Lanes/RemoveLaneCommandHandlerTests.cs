using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Commands.RemoveLane;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveLaneCommandHandlerTests
    {
        private readonly Mock<ILanesRepository> _mockLanesRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<RemoveLaneCommand> _handler;

        public RemoveLaneCommandHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RemoveLaneCommandHandler(_mockLanesRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectLaneId()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            var command = new RemoveLaneCommand(laneId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockLanesRepository.Verify(r => r.Delete(laneId), Times.Once);
        }
    }
}
