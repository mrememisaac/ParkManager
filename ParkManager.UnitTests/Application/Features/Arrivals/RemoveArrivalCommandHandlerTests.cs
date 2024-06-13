using AutoMapper;
using Moq;
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

        public RemoveArrivalCommandHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectArrivalId()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            var command = new RemoveArrivalCommand(arrivalId);
            var handler = new RemoveArrivalCommandHandler(_mockArrivalsRepository.Object, _mockMapper.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArrivalsRepository.Verify(r => r.Delete(arrivalId), Times.Once);
        }
    }
}
