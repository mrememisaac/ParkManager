using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class AddVehicleCommandHandlerTests
    {
        private readonly Mock<IVehiclesRepository> _mockVehiclesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<AddVehicleCommandHandler>> _mockLogger;
        private readonly AddVehicleCommandValidator _validator;
        private readonly AddVehicleCommand _command;
        private readonly AddVehicleCommandHandler _handler;

        public AddVehicleCommandHandlerTests()
        {
            _mockVehiclesRepository = new Mock<IVehiclesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new AddVehicleCommandValidator();
            _command = CreateAddVehicleCommand();
            _mockLogger = new Mock<ILogger<AddVehicleCommandHandler>>();
            _handler = new AddVehicleCommandHandler(_mockVehiclesRepository.Object, _validator, _mockMapper.Object, _mockLogger.Object);
        }

        private AddVehicleCommand CreateAddVehicleCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new AddVehicleCommand()
                {
                    Id = Guid.NewGuid(),
                    Make = "Toyota",
                    Model = "Corolla",
                    Registration = "XYZ123"
                };
            }
            return new AddVehicleCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var vehicle = new Vehicle(command.Id, command.Make, command.Model, command.Registration);
            
            var expectedResponse = new AddVehicleCommandResponse
            {
                Id = vehicle.Id,
                Registration = vehicle.Registration,
                Make = vehicle.Make,
                Model = vehicle.Model
            };
            _mockMapper.Setup(m => m.Map<Vehicle>(It.IsAny<AddVehicleCommand>())).Returns(vehicle);
            _mockMapper.Setup(m => m.Map<AddVehicleCommandResponse>(It.IsAny<Vehicle>())).Returns(expectedResponse);
            _mockVehiclesRepository.Setup(repo => repo.Add(It.IsAny<Vehicle>())).ReturnsAsync(vehicle);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Model, result.Model);
            Assert.Equal(expectedResponse.Make, result.Make);
            Assert.Equal(expectedResponse.Registration, result.Registration);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new AddVehicleCommand(); // Invalid because required properties are not set

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WithMappedVehicle()
        {
            // Arrange
            var command = _command;
            var mappedVehicle = new Vehicle(command.Id, command.Make, command.Model, command.Registration);
            _mockMapper.Setup(m => m.Map<Vehicle>(It.IsAny<AddVehicleCommand>())).Returns(mappedVehicle);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockVehiclesRepository.Verify(r => r.Add(mappedVehicle), Times.Once);
        }
    }
}

