using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Commands.RemoveOccasion;

namespace ParkManager.UnitTests
{
    public class RemoveOccasionCommandHandlerTests
    {
        private readonly Mock<IOccasionsRepository> _mockOccasionsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<RemoveOccasionCommand> _handler;

        public RemoveOccasionCommandHandlerTests()
        {
            _mockOccasionsRepository = new Mock<IOccasionsRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RemoveOccasionCommandHandler(_mockOccasionsRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectOccasionId()
        {
            // Arrange
            var occasionId = Guid.NewGuid();
            var command = new RemoveOccasionCommand(occasionId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockOccasionsRepository.Verify(r => r.Delete(occasionId), Times.Once);
        }
    }
}