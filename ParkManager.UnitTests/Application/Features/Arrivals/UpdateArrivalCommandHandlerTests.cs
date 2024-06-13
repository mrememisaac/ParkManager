using AutoMapper;
using FluentValidation;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class UpdateArrivalCommandHandlerTests
    {
        private readonly Mock<IArrivalsRepository> _mockArrivalsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateArrivalCommandValidator _validator;
        private readonly UpdateArrivalCommand _command;

        public UpdateArrivalCommandHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateArrivalCommandValidator();
            _command = CreateUpdateArrivalCommand();
        }

        private UpdateArrivalCommand CreateUpdateArrivalCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new UpdateArrivalCommand()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    VehicleId = Guid.NewGuid(),
                    DriverId = Guid.NewGuid(),
                    TagId = Guid.NewGuid(),
                    ParkId = Guid.NewGuid()
                };
            }
            return new UpdateArrivalCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var handler = new UpdateArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);
            var arrival = new Arrival(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            var expectedResponse = new UpdateArrivalCommandResponse()
            {
                Id = arrival.Id,
                DriverId = arrival.DriverId,
                VehicleId = arrival.VehicleId,
                TagId = arrival.TagId,
                ParkId = arrival.ParkId
            };
            _mockMapper.Setup(m => m.Map<Arrival>(It.IsAny<UpdateArrivalCommand>())).Returns(arrival);
            _mockMapper.Setup(m => m.Map<UpdateArrivalCommandResponse>(It.IsAny<Arrival>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = CreateUpdateArrivalCommand(false);

            var handler = new UpdateArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedArrival()
        {
            // Arrange
            var command = _command;
            var handler = new UpdateArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object);
            var mappedArrival = new Arrival(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            _mockMapper.Setup(m => m.Map<Arrival>(It.IsAny<UpdateArrivalCommand>())).Returns(mappedArrival);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArrivalsRepository.Verify(r => r.Update(mappedArrival), Times.Once);
        }
    }
}
