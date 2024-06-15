using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class UpdateVehicleCommandHandlerTests
    {
        private readonly Mock<IVehiclesRepository> _mockVehiclesRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateVehicleCommandValidator _validator;
        private readonly UpdateVehicleCommand _command;

        public UpdateVehicleCommandHandlerTests()
        {
            _mockVehiclesRepository = new Mock<IVehiclesRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new UpdateVehicleCommandValidator();
            _command = CreateUpdateVehicleCommand();
        }

        private UpdateVehicleCommand CreateUpdateVehicleCommand(bool validInstance = true)
        {
            if (validInstance)
            {
                return new UpdateVehicleCommand()
                {
                    Id = Guid.NewGuid(),
                    Make = "Toyota",
                    Model = "Corolla",
                    Registration = "XYZ123"
                };
            }
            return new UpdateVehicleCommand();
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResponse_WhenCommandIsValid()
        {
            // Arrange
            var command = _command;
            var vehicle = new Vehicle(command.Id, command.Make, command.Model, command.Registration);
            var handler = new UpdateVehicleCommandHandler(_mockVehiclesRepository.Object, _validator, _mockMapper.Object);
            var expectedResponse = new UpdateVehicleCommandResponse
            {
                Id = vehicle.Id,
                Registration = vehicle.Registration,
                Make = vehicle.Make,
                Model = vehicle.Model
            };
            _mockMapper.Setup(m => m.Map<Vehicle>(It.IsAny<UpdateVehicleCommand>())).Returns(vehicle);
            _mockMapper.Setup(m => m.Map<UpdateVehicleCommandResponse>(It.IsAny<Vehicle>())).Returns(expectedResponse);
            _mockVehiclesRepository.Setup(repo => repo.Update(It.IsAny<Vehicle>())).ReturnsAsync(vehicle);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

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
            var command = new UpdateVehicleCommand(); // Invalid because required properties are not set
            var handler = new UpdateVehicleCommandHandler(_mockVehiclesRepository.Object, _validator, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WithMappedVehicle()
        {
            // Arrange
            var command = _command;
            var mappedVehicle = new Vehicle(command.Id, command.Make, command.Model, command.Registration);
            _mockMapper.Setup(m => m.Map<Vehicle>(It.IsAny<UpdateVehicleCommand>())).Returns(mappedVehicle);
            var handler = new UpdateVehicleCommandHandler(_mockVehiclesRepository.Object, _validator, _mockMapper.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mockVehiclesRepository.Verify(r => r.Update(mappedVehicle), Times.Once);
        }
    }
}

