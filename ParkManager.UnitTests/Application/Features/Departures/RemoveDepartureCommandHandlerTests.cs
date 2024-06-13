using AutoMapper;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Commands.RemoveDeparture;

namespace ParkManager.UnitTests
{


    public class RemoveDepartureCommandHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly Mock<IMapper> _mockMapper;

        public RemoveDepartureCommandHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectDepartureId()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            var command = new RemoveDepartureCommand(departureId);
            var handler = new RemoveDepartureCommandHandler(_mockDeparturesRepository.Object, _mockMapper.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDeparturesRepository.Verify(r => r.Delete(departureId), Times.Once);
        }
    }
}