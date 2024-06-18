using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class UpdateSlotCommandHandlerTests
    {
        private readonly Mock<ISlotsRepository> _mockSlotsRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<UpdateSlotCommandHandler>> _mockLogger;
        private readonly UpdateSlotCommandValidator _validator;
        private readonly UpdateSlotCommand _command;
        private readonly UpdateSlotCommandHandler _handler;

        public UpdateSlotCommandHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateSlotCommandValidator();
            _command = CreateUpdateSlotCommand();
            _mockLogger = new Mock<ILogger<UpdateSlotCommandHandler>>();
            _handler = new UpdateSlotCommandHandler(_mockSlotsRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private UpdateSlotCommand CreateUpdateSlotCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new UpdateSlotCommand()
                {
                    Id = Guid.NewGuid(),
                    LaneId = Guid.NewGuid(),
                    Name = "A1"
                };
            }
            return new UpdateSlotCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var slot = new Slot(command.Id, command.LaneId, command.Name);
            var expectedResponse = new UpdateSlotCommandResponse
            {
                Id = slot.Id,
                LaneId = slot.LaneId,
                Name = slot.Name
            };
            _mockMapper.Setup(m => m.Map<Slot>(It.IsAny<UpdateSlotCommand>())).Returns(slot);
            _mockMapper.Setup(m => m.Map<UpdateSlotCommandResponse>(It.IsAny<Slot>())).Returns(expectedResponse);
            _mockSlotsRepository.Setup(repo => repo.Update(slot)).ReturnsAsync(slot);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            // Additional assertions for response properties if necessary
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new UpdateSlotCommand(); // Populate with invalid properties

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedSlot()
        {
            // Arrange
            var command = _command;
            var mappedSlot = new Slot(command.Id, command.LaneId, command.Name); // Adjust mapping as necessary
            _mockMapper.Setup(m => m.Map<Slot>(It.IsAny<UpdateSlotCommand>())).Returns(mappedSlot);


            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockSlotsRepository.Verify(r => r.Update(mappedSlot), Times.Once);
        }
    }
}
