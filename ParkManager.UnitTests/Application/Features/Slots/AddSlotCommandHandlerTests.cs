using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddSlotCommandHandlerTests
    {
        private readonly Mock<ISlotsRepository> _mockSlotsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddSlotCommandValidator _validator;
        private readonly AddSlotCommand _command;

        public AddSlotCommandHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddSlotCommandValidator();
            _command = CreateAddSlotCommand();
        }

        private AddSlotCommand CreateAddSlotCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddSlotCommand()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Name",
                    LaneId = Guid.NewGuid()
                };
            }
            return new AddSlotCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;

            var slot = new Slot(command.Id, command.LaneId, command.Name); // Populate with valid properties

            var handler = new AddSlotCommandHandler(_mockSlotsRepository.Object, _validator, _mockMapper.Object);

            var expectedResponse = new AddSlotCommandResponse
            {
                Id = slot.Id,
                LaneId = slot.LaneId,
                Name = slot.Name
            };
            _mockMapper.Setup(m => m.Map<Slot>(It.IsAny<AddSlotCommand>())).Returns(slot); // Adjust mapping as necessary
            _mockMapper.Setup(m => m.Map<AddSlotCommandResponse>(It.IsAny<Slot>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddSlotCommand(); // Populate with invalid properties

            var handler = new AddSlotCommandHandler(_mockSlotsRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedSlot()
        {
            // Arrange
            var command = _command;

            var handler = new AddSlotCommandHandler(_mockSlotsRepository.Object, _validator, _mockMapper.Object);
            var mappedSlot = new Slot(command.Id, command.LaneId, command.Name); // Adjust mapping as necessary

            _mockMapper.Setup(m => m.Map<Slot>(It.IsAny<AddSlotCommand>())).Returns(mappedSlot);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockSlotsRepository.Verify(r => r.Add(mappedSlot), Times.Once);
        }
    }
}
