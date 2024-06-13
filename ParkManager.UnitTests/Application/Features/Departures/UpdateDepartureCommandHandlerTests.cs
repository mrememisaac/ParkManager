using AutoMapper;
using FluentValidation;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class UpdateDepartureCommandHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateDepartureCommandValidator _validator;
        private readonly UpdateDepartureCommand _command;

        public UpdateDepartureCommandHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateDepartureCommandValidator();
            _command = CreateUpdateDepartureCommand();
        }

        private UpdateDepartureCommand CreateUpdateDepartureCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new UpdateDepartureCommand()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    VehicleId = Guid.NewGuid(),
                    DriverId = Guid.NewGuid(),
                    TagId = Guid.NewGuid(),
                    ParkId = Guid.NewGuid()
                };
            }
            return new UpdateDepartureCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var handler = new UpdateDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);
            var departure = new Departure(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            var expectedResponse = new UpdateDepartureCommandResponse()
            {
                Id = departure.Id,
                DriverId = departure.DriverId,
                VehicleId = departure.VehicleId,
                TagId = departure.TagId,
                ParkId = departure.ParkId
            };
            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<UpdateDepartureCommand>())).Returns(departure);
            _mockMapper.Setup(m => m.Map<UpdateDepartureCommandResponse>(It.IsAny<Departure>())).Returns(expectedResponse);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = CreateUpdateDepartureCommand(false);

            var handler = new UpdateDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedDeparture()
        {
            // Arrange
            var command = _command;
            var handler = new UpdateDepartureCommandHandler(_mockDeparturesRepository.Object, _validator, _mockMapper.Object);
            var mappedDeparture = new Departure(command.Id, command.Timestamp, command.ParkId, command.VehicleId, command.DriverId, command.TagId);

            _mockMapper.Setup(m => m.Map<Departure>(It.IsAny<UpdateDepartureCommand>())).Returns(mappedDeparture);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDeparturesRepository.Verify(r => r.Update(mappedDeparture), Times.Once);
        }
    }
}
