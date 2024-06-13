using AutoMapper;
using FluentValidation;
using MediatR;
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
        private readonly AddLaneCommandValidator _validator;
        private readonly AddLaneCommand _command;

        public AddLaneCommandHandlerTests()
        {
            _mockLanesRepository = new Mock<ILanesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddLaneCommandValidator();
            _command = new AddLaneCommand
            {
                Id = Guid.NewGuid(), 
                Name = "Lane 1", 
                ParkId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var lane = new Lane(command.Id, command.ParkId, command.Name);
            var handler = new AddLaneCommandHandler(_mockLanesRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new AddLaneCommandResponse()
            {
                Id = lane.Id,
                ParkId = lane.ParkId,
                Name = lane.Name
            };
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<AddLaneCommand>())).Returns(lane);
            _mockMapper.Setup(m => m.Map<AddLaneCommandResponse>(It.IsAny<Lane>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddLaneCommand();
            var handler = new AddLaneCommandHandler(_mockLanesRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedLane()
        {
            // Arrange
            var command = _command;
            var handler = new AddLaneCommandHandler(_mockLanesRepository.Object, _validator, _mockMapper.Object);
            var mappedLane = new Lane(command.Id, command.ParkId, command.Name);
            _mockMapper.Setup(m => m.Map<Lane>(It.IsAny<AddLaneCommand>())).Returns(mappedLane);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockLanesRepository.Verify(r => r.Add(mappedLane), Times.Once);
        }
    }
}
