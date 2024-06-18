using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Commands.RemoveArrival;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveArrivalCommandHandlerTests
    {
        private readonly Mock<IArrivalsRepository> _mockArrivalsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<RemoveArrivalCommandHandler>> _mockLogger;
        private readonly RemoveArrivalCommandHandler _handler;

        public RemoveArrivalCommandHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RemoveArrivalCommandHandler>>();
            _handler = new RemoveArrivalCommandHandler(_mockArrivalsRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectArrivalId()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            var command = new RemoveArrivalCommand(arrivalId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArrivalsRepository.Verify(r => r.Delete(arrivalId), Times.Once);
        }
    }
}
