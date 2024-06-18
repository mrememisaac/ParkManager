using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Application.Features.Arrivals.Commands.RemoveArrival;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Application.Features.Arrivals.Queries.GetArrivals;
using ParkManager.Application.Features.Parks.Queries.GetPark;

namespace ParkManager.UnitTests
{
    public class ArrivalsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ArrivalsController _controller;
        private readonly GetArrivalQueryResponse _arrivalResponse;
        private readonly Mock<ILogger<ArrivalsController>> _mockLogger;

        public ArrivalsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ArrivalsController>>();
            _controller = new ArrivalsController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithArrival_WhenExists()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            var arrivalResponse = new GetArrivalQueryResponse
            {
                Id = arrivalId,
                DriverId = Guid.NewGuid(),
                ParkId = Guid.NewGuid(),
                VehicleId = Guid.NewGuid(),
                TagId = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetArrivalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(arrivalResponse);
            
            // Act
            var result = await _controller.Get(arrivalId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(arrivalResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenArrivalDoesNotExist()
        {
            var response = (GetArrivalQueryResponse)null;

            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetArrivalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithArrivals()
        {
            // Arrange
            var arrivalsResponse = new GetArrivalsQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetArrivalsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(arrivalsResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(arrivalsResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithArrivalId()
        {
            // Arrange
            Arrival arrival = CreateArrival("Arrival 1");
            _mockMapper.Setup(m => m.Map<AddArrivalCommand>(arrival)).Returns(
                new AddArrivalCommand
                {
                    Id = arrival.Id,
                    ParkId = arrival.ParkId,
                    DriverId = arrival.DriverId,
                    TagId = arrival.TagId,
                    Timestamp = arrival.Timestamp,
                    VehicleId = arrival.VehicleId
                }
                );
            var commandResponse = new AddArrivalCommandResponse
            {
                Id = arrival.Id,
                ParkId = arrival.ParkId,
                DriverId = arrival.DriverId,
                TagId = arrival.TagId,
                Timestamp = arrival.Timestamp,
                VehicleId = arrival.VehicleId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddArrivalCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(arrival);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        private static Arrival CreateArrival(string name)
        {
            var arrival = new Arrival(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            return arrival;
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            Arrival arrival = CreateArrival("Arrival 2");
            _mockMapper.Setup(m => m.Map<UpdateArrivalCommand>(arrival)).Returns(new UpdateArrivalCommand
            {
                Id = arrival.Id,
                ParkId = arrival.ParkId,
                DriverId = arrival.DriverId,
                TagId = arrival.TagId,
                Timestamp = arrival.Timestamp,
                VehicleId = arrival.VehicleId
            });
            var commandResponse = new UpdateArrivalCommandResponse
            {
                Id = arrival.Id,
                ParkId = arrival.ParkId,
                DriverId = arrival.DriverId,
                TagId = arrival.TagId,
                Timestamp = arrival.Timestamp,
                VehicleId = arrival.VehicleId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateArrivalCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(arrival);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var arrivalId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveArrivalCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(arrivalId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

