using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddLaneCommandHandlerTests
    {
        private readonly Mock<ILanesRepository> _mockLanesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<AddLaneCommandHandler>> _mockLogger;
        private readonly AddLaneCommandValidator _validator;
        private readonly AddLaneCommand _command;
        private readonly AddLaneCommandHandler _handler;

        public AddLaneCommandHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddLaneCommandValidator();
            _mockLogger = new Mock<ILogger<AddLaneCommandHandler>>();
            _command = new AddLaneCommand
            {
                Id = Guid.NewGuid(), 
                Name = "Lane 1", 
                ParkId = Guid.NewGuid()
            };
            _handler = new AddLaneCommandHandler(_mockLanesRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var lane = new Lane(command.Id, command.ParkId, command.Name);
            var expectedResponse = new AddLaneCommandResponse()
            {
                Id = lane.Id,
                ParkId = lane.ParkId,
                Name = lane.Name
            };
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<AddLaneCommand>())).Returns(lane);
            _mockMapper.Setup(m => m.Map<AddLaneCommandResponse>(It.IsAny<Lane>())).Returns(expectedResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddLaneCommand();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedLane()
        {
            // Arrange
            var command = _command;
            var mappedLane = new Lane(command.Id, command.ParkId, command.Name);
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<AddLaneCommand>())).Returns(mappedLane);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockLanesRepository.Verify(r => r.Add(mappedLane), Times.Once);
        }
    }
}
