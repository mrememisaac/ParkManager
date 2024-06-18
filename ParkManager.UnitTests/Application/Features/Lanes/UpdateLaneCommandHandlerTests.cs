using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class UpdateLaneCommandHandlerTests
    {
        private readonly Mock<ILanesRepository> _mockLanesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<UpdateLaneCommandHandler>> _mockLogger;
        private readonly UpdateLaneCommandValidator _validator;
        private readonly UpdateLaneCommandHandler _handler;
        private readonly UpdateLaneCommand _command;

        public UpdateLaneCommandHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateLaneCommandValidator();
            _mockLogger = new Mock<ILogger<UpdateLaneCommandHandler>>();
            _handler = new UpdateLaneCommandHandler(_mockLanesRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
            _command = new UpdateLaneCommand
            {
                Id = Guid.NewGuid(),
                ParkId = Guid.NewGuid(),
                Name = "Lane 1"
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var lane = new Lane(command.Id, command.ParkId, command.Name);
            var response = new UpdateLaneCommandResponse()
            {
                Id = command.Id,
                ParkId = command.ParkId,
                Name = command.Name
            };
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<UpdateLaneCommand>())).Returns(lane);
            _mockMapper.Setup(m => m.Map<UpdateLaneCommandResponse>(It.IsAny<Lane>())).Returns(response);
            _mockLanesRepository.Setup(repo => repo.Update(It.IsAny<Lane>())).ReturnsAsync(lane);

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
            var command = new UpdateLaneCommand(); // Populate with invalid properties

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _validator.ValidateAndThrowAsync(command));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedLane()
        {
            // Arrange
            var command = _command;
            var mappedLane = new Lane(command.Id, command.ParkId, command.Name);
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<UpdateLaneCommand>())).Returns(mappedLane);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockLanesRepository.Verify(r => r.Update(mappedLane), Times.Once);
        }
    }
}
