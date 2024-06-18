using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        private readonly Mock<ILogger<UpdateArrivalCommandHandler>> _mockLogger;
        private readonly UpdateArrivalCommandValidator _validator;
        private readonly UpdateArrivalCommand _command;
        private readonly UpdateArrivalCommandHandler _handler;

        public UpdateArrivalCommandHandlerTests()
        {
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateArrivalCommandValidator();
            _command = CreateUpdateArrivalCommand();
            _mockLogger = new Mock<ILogger<UpdateArrivalCommandHandler>>();
            _handler = new UpdateArrivalCommandHandler(_mockArrivalsRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object); 

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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = CreateUpdateArrivalCommand(false);


            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedArrival()
        {
            // Arrange
            var command = _command;
            var mappedArrival = new Arrival(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            _mockMapper.Setup(m => m.Map<Arrival>(It.IsAny<UpdateArrivalCommand>())).Returns(mappedArrival);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArrivalsRepository.Verify(r => r.Update(mappedArrival), Times.Once);
        }
    }
}
