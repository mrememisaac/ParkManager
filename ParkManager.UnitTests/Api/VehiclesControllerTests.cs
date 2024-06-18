using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.RemoveVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicles;
using ParkManager.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class VehiclesControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly VehiclesController _controller;
        private readonly Mock<ILogger<VehiclesController>> _mockLogger;

        public VehiclesControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<VehiclesController>>();
            _controller = new VehiclesController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetVehicle_ReturnsOk_WithVehicle_WhenExists()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicleResponse = new GetVehicleQueryResponse { Id = vehicleId, Make = "TestMake", Model = "TestModel", Registration = "TestRegistration" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetVehicleQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(vehicleResponse);

            // Act
            var result = await _controller.Get(vehicleId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(vehicleResponse);
        }

        [Fact]
        public async Task GetVehicle_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetVehicleQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetVehicleQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddVehicle_ReturnsCreatedAtAction_WithVehicleId()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Api.Models.Vehicle(vehicleId, "NewMake", "NewModel", "NewRegistration");
            var commandResponse = new AddVehicleCommandResponse { Id = vehicleId, Make = "NewMake", Model = "NewModel", Registration = "NewRegistration" };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddVehicleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(vehicle);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        [Fact]
        public async Task UpdateVehicle_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Api.Models.Vehicle(vehicleId, "UpdatedMake", "UpdatedModel", "UpdatedRegistration");
            
            var commandResponse = new UpdateVehicleCommandResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateVehicleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(vehicle);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteVehicle_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();

            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveVehicleCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(vehicleId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
