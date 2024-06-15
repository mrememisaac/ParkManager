using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.RemoveLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Application.Features.Lanes.Queries.GetLane;
using ParkManager.Application.Features.Lanes.Queries.GetLanes;

namespace ParkManager.UnitTests
{
    public class LanesControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LanesController _controller;

        public LanesControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _controller = new LanesController(_mockMediator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithLane_WhenExists()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            var laneResponse = new GetLaneQueryResponse
            {
                Id = laneId,
                Name = "TestName",
                ParkId = Guid.NewGuid()
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLaneQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(laneResponse);

            // Act
            var result = await _controller.Get(laneId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(laneResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenLaneDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLaneQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetLaneQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithLanes()
        {
            // Arrange
            var lanesResponse = new GetLanesQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLanesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(lanesResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(lanesResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithLaneId()
        {
            // Arrange
            Lane lane = CreateLane("Lane 1");
            _mockMapper.Setup(m => m.Map<AddLaneCommand>(lane)).Returns(
                new AddLaneCommand
                {
                    Id = lane.Id,
                    Name = lane.Name,
                    ParkId = lane.ParkId
                }
                );
            var commandResponse = new AddLaneCommandResponse
            {
                Id = lane.Id,
                Name = lane.Name,
                ParkId = lane.ParkId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddLaneCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(lane);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        private static Lane CreateLane(string name)
        {
            var id = Guid.NewGuid();
            var parkId = Guid.NewGuid();

            var lane = new Lane(id, parkId, name);
            return lane;
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            Lane lane = CreateLane("Lane 2");
            _mockMapper.Setup(m => m.Map<UpdateLaneCommand>(lane)).Returns(new UpdateLaneCommand
            {
                Id = lane.Id,
                Name = lane.Name,
                ParkId = lane.ParkId
            });
            var commandResponse = new UpdateLaneCommandResponse
            {
                Id = lane.Id,
                Name = lane.Name,
                ParkId = lane.ParkId
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateLaneCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(lane);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var laneId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveLaneCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(laneId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

