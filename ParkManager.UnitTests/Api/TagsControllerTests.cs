using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Tags.Commands.RemoveTag;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Application.Features.Tags.Queries.GetTags;

namespace ParkManager.UnitTests
{
    public class TagsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TagsController _controller;
        private readonly Mock<ILogger<TagsController>> _mockLogger;

        public TagsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<TagsController>>();
            _controller = new TagsController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithTag_WhenExists()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            var tagResponse = new GetTagQueryResponse { Id = tagId, Number = 123 };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetTagQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tagResponse);

            // Act
            var result = await _controller.Get(tagId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(tagResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenTagDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetTagQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetTagQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithTags()
        {
            // Arrange
            var tagsResponse = new GetTagsQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetTagsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tagsResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(tagsResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithTagId()
        {
            // Arrange
            var id = Guid.NewGuid(); 
            var tag = new Tag(id, 123);
            var commandResponse = new AddTagCommandResponse { Id = id, Number = 123 };
            _mockMapper.Setup(m => m.Map<AddTagCommand>(tag)).Returns(new AddTagCommand { Number = tag.Number });
            _mockMediator.Setup(m => m.Send(It.IsAny<AddTagCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(tag);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var tag = new Tag(id, 123);
            _mockMapper.Setup(m => m.Map<UpdateTagCommand>(tag)).Returns(new UpdateTagCommand { Id = tag.Id, Number = tag.Number });
            var commandResponse = new UpdateTagCommandResponse { Id = tag.Id, Number = tag.Number };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateTagCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(tag);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveTagCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(tagId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
