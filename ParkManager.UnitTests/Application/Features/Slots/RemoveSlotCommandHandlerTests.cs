using AutoMapper;
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

        public RemoveSlotCommandHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectSlotId()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var command = new RemoveSlotCommand(slotId);
            var handler = new RemoveSlotCommandHandler(_mockSlotsRepository.Object, _mockMapper.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockSlotsRepository.Verify(r => r.Delete(slotId), Times.Once);
        }
    }
}
