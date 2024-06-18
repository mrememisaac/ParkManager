using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Commands.RemoveSlot;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveSlotCommandHandlerTests
    {
        private readonly Mock<ISlotsRepository> _mockSlotsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<RemoveSlotCommandHandler>> _mockLogger;
        private readonly RemoveSlotCommandHandler _handler;

        public RemoveSlotCommandHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<RemoveSlotCommandHandler>>();
            _handler = new RemoveSlotCommandHandler(_mockSlotsRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectSlotId()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var command = new RemoveSlotCommand(slotId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockSlotsRepository.Verify(r => r.Delete(slotId), Times.Once);
        }
    }
}
