using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class RemoveVehicleCommandHandlerTests
    {
        private readonly Mock<IVehiclesRepository> _mockVehiclesRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<RemoveVehicleCommand> _handler;

        public RemoveVehicleCommandHandlerTests()
        {
            _mockVehiclesRepository = new Mock<IVehiclesRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RemoveVehicleCommandHandler(_mockVehiclesRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WithCorrectVehicleId()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var command = new RemoveVehicleCommand (vehicleId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockVehiclesRepository.Verify(r => r.Delete(vehicleId), Times.Once);
        }
    }
}

