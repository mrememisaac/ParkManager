using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Occasions.Commands.RemoveOccasion;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Application.Features.Occasions.Queries.GetOccasions;

namespace ParkManager.UnitTests
{
    public class OccasionsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OccasionsController _controller;

        public OccasionsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _controller = new OccasionsController(_mockMediator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithOccasion_WhenExists()
        {
            // Arrange
            var occasionId = Guid.NewGuid();
            var occasionResponse = new GetOccasionQueryResponse
            {
                Id = occasionId,
                Name = "TestName",
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetOccasionQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(occasionResponse);

            // Act
            var result = await _controller.Get(occasionId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(occasionResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenOccasionDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetOccasionQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetOccasionQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithOccasions()
        {
            // Arrange
            var occasionsResponse = new GetOccasionsQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetOccasionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(occasionsResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(occasionsResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithOccasionId()
        {
            // Arrange
            Occasion occasion = CreateOccasion("Occasion 1");
            _mockMapper.Setup(m => m.Map<AddOccasionCommand>(occasion)).Returns(
                new AddOccasionCommand
                {
                    Id = occasion.Id,
                    EndDate = occasion.EndDate,
                    Name = occasion.Name,
                    StartDate = occasion.StartDate
                }
                );
            var commandResponse = new AddOccasionCommandResponse
            {
                Id = occasion.Id,
                EndDate = occasion.EndDate,
                Name = occasion.Name,
                StartDate = occasion.StartDate
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddOccasionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(occasion);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        private static Occasion CreateOccasion(string name)
        {
            var id = Guid.NewGuid();
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            // Act
            var occasion = new Occasion(id, name, startDate, endDate);
            return occasion;
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            Occasion occasion = CreateOccasion("Occasion 2");
            _mockMapper.Setup(m => m.Map<UpdateOccasionCommand>(occasion)).Returns(new UpdateOccasionCommand
            {
                Id = occasion.Id,
                EndDate = occasion.EndDate,
                Name = occasion.Name,
                StartDate = occasion.StartDate
            });
            var commandResponse = new UpdateOccasionCommandResponse
            {
                Id = occasion.Id,
                EndDate = occasion.EndDate,
                Name = occasion.Name,
                StartDate = occasion.StartDate
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateOccasionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(occasion);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var occasionId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveOccasionCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(occasionId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

