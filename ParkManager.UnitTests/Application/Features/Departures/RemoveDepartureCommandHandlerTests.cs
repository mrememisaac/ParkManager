using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Commands.RemoveDeparture;

namespace ParkManager.UnitTests
{


    public class RemoveDepartureCommandHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<RemoveDepartureCommandHandler>> _mockLogger;
        private readonly RemoveDepartureCommandHandler _handler;

        public RemoveDepartureCommandHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RemoveDepartureCommandHandler>>();
            _handler = new RemoveDepartureCommandHandler(_mockDeparturesRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectDepartureId()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            var command = new RemoveDepartureCommand(departureId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDeparturesRepository.Verify(r => r.Delete(departureId), Times.Once);
        }
    }
}