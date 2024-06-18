using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.RemoveDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Application.Features.Drivers.Queries.GetDrivers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class DriversControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DriversController _controller;
        private readonly Mock<ILogger<DriversController>> _mockLogger;

        public DriversControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<DriversController>>();
            _controller = new DriversController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithDriver_WhenExists()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driverResponse = new GetDriverQueryResponse { Id = driverId, Name = "TestDriver" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDriverQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(driverResponse);

            // Act
            var result = await _controller.Get(driverId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(driverResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenDriverDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDriverQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetDriverQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithDrivers()
        {
            // Arrange
            var driversResponse = new GetDriversQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDriversQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(driversResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(driversResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithDriverId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var driver = new Driver(id, "NewDriver", "12345678");
            var commandResponse = new AddDriverCommandResponse { Id = driver.Id, Name = driver.Name, PhoneNumber = driver.PhoneNumber };
            _mockMapper.Setup(m => m.Map<AddDriverCommand>(driver)).Returns(new AddDriverCommand { Name = driver.Name });
            _mockMediator.Setup(m => m.Send(It.IsAny<AddDriverCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(driver);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var driver = new Driver(Guid.NewGuid(), "UpdatedDriver", "12345678");
            _mockMapper.Setup(m => m.Map<UpdateDriverCommand>(driver)).Returns(new UpdateDriverCommand { Id = driver.Id, Name = driver.Name });
            var commandResponse = new UpdateDriverCommandResponse { Id = driver.Id, Name = driver.Name };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(driver);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveDriverCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(driverId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

