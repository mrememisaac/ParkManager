using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.RemoveDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Application.Features.Departures.Queries.GetDepartures;
using ParkManager.Application.Features.Parks.Queries.GetPark;

namespace ParkManager.UnitTests
{
    public class DeparturesControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DeparturesController _controller;
        private readonly GetDepartureQueryResponse _departureResponse;

        public DeparturesControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _controller = new DeparturesController(_mockMediator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithDeparture_WhenExists()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            var departureResponse = new GetDepartureQueryResponse
            {
                Id = departureId,
                DriverId = Guid.NewGuid(),
                ParkId = Guid.NewGuid(),
                VehicleId = Guid.NewGuid(),
                TagId = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDepartureQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(departureResponse);
            
            // Act
            var result = await _controller.Get(departureId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(departureResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenDepartureDoesNotExist()
        {
            var response = (GetDepartureQueryResponse)null;

            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDepartureQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithDepartures()
        {
            // Arrange
            var departuresResponse = new GetDeparturesQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetDeparturesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(departuresResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(departuresResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithDepartureId()
        {
            // Arrange
            Departure departure = CreateDeparture("Departure 1");
            _mockMapper.Setup(m => m.Map<AddDepartureCommand>(departure)).Returns(
                new AddDepartureCommand
                {
                    Id = departure.Id,
                    ParkId = departure.ParkId,
                    DriverId = departure.DriverId,
                    TagId = departure.TagId,
                    Timestamp = departure.Timestamp,
                    VehicleId = departure.VehicleId
                }
                );
            var commandResponse = new AddDepartureCommandResponse
            {
                Id = departure.Id,
                ParkId = departure.ParkId,
                DriverId = departure.DriverId,
                TagId = departure.TagId,
                Timestamp = departure.Timestamp,
                VehicleId = departure.VehicleId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddDepartureCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(departure);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        private static Departure CreateDeparture(string name)
        {
            var departure = new Departure(Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            return departure;
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            Departure departure = CreateDeparture("Departure 2");
            _mockMapper.Setup(m => m.Map<UpdateDepartureCommand>(departure)).Returns(new UpdateDepartureCommand
            {
                Id = departure.Id,
                ParkId = departure.ParkId,
                DriverId = departure.DriverId,
                TagId = departure.TagId,
                Timestamp = departure.Timestamp,
                VehicleId = departure.VehicleId
            });
            var commandResponse = new UpdateDepartureCommandResponse
            {
                Id = departure.Id,
                ParkId = departure.ParkId,
                DriverId = departure.DriverId,
                TagId = departure.TagId,
                Timestamp = departure.Timestamp,
                VehicleId = departure.VehicleId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateDepartureCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(departure);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var departureId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveDepartureCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(departureId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

