using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Drivers.Commands.RemoveDriver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveDriverCommandHandlerTests
    {
        private readonly Mock<IDriversRepository> _mockDriversRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<RemoveDriverCommandHandler>> _mockLogger;
        private readonly IRequestHandler<RemoveDriverCommand> _handler;

        public RemoveDriverCommandHandlerTests()
        {
            _mockDriversRepository = new Mock<IDriversRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RemoveDriverCommandHandler>>();
            _handler = new RemoveDriverCommandHandler(_mockDriversRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectDriverId()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var command = new RemoveDriverCommand(driverId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDriversRepository.Verify(r => r.Delete(driverId), Times.Once);
        }
    }
}
