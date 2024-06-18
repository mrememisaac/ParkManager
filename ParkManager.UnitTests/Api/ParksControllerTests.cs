using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Parks.Commands.RemovePark;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Application.Features.Parks.Queries.GetParks;

namespace ParkManager.UnitTests
{
    public class ParksControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ParksController _controller;
        private readonly Mock<ILogger<ParksController>> _mockLogger;

        public ParksControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ParksController>>();
            _controller = new ParksController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithPark_WhenExists()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            var parkResponse = new GetParkQueryResponse
            {
                Id = parkId,
                Name = "TestName",
                City = "TestCity",
                Country = "TestCountry",
                Latitude = 1.0,
                Longitude = 1.0,
                State = "TestState",
                Street = "TestStreet"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetParkQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(parkResponse);

            // Act
            var result = await _controller.Get(parkId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(parkResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenParkDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetParkQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetParkQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithParks()
        {
            // Arrange
            var parksResponse = new GetParksQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetParksQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(parksResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(parksResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithParkId()
        {
            // Arrange
            Park park = CreatePark("Park 1");
            _mockMapper.Setup(m => m.Map<AddParkCommand>(park)).Returns(
                new AddParkCommand
                {
                    Id = park.Id,
                    Street = park.Street,
                    City = park.City,
                    State = park.State,
                    Country = park.Country,
                    Latitude = park.Latitude,
                    Longitude = park.Longitude
                }
                );
            var commandResponse = new AddParkCommandResponse
            {
                Id = park.Id,
                Street = park.Street,
                City = park.City,
                State = park.State,
                Country = park.Country,
                Latitude = park.Latitude,
                Longitude = park.Longitude
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddParkCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(park);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        private static Park CreatePark(string name)
        {
            var id = Guid.NewGuid();
            var street = "Street 1";
            var city = "City 1";
            var state = "State 1";
            var country = "Country 1";
            long latitude = 123456789;
            long longitude = 987654321;

            var park = new Park(id, name, street, city, state, country, latitude, longitude);
            return park;
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            Park park = CreatePark("Park 2");
            _mockMapper.Setup(m => m.Map<UpdateParkCommand>(park)).Returns(new UpdateParkCommand {
                Id = park.Id,
                Street = park.Street,
                City = park.City,
                State = park.State,
                Country = park.Country,
                Latitude = park.Latitude,
                Longitude = park.Longitude
            });
            var commandResponse = new UpdateParkCommandResponse {
                Id = park.Id,
                Street = park.Street,
                City = park.City,
                State = park.State,
                Country = park.Country,
                Latitude = park.Latitude,
                Longitude = park.Longitude
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateParkCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(park);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var parkId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveParkCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(parkId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

