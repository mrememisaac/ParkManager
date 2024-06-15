using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkManager.Api.Controllers;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Slots.Commands.RemoveSlot;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Application.Features.Slots.Queries.GetSlots;

namespace ParkManager.UnitTests
{
    public class SlotsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SlotsController _controller;

        public SlotsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SlotsController(_mockMediator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithSlot_WhenExists()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var laneId = Guid.NewGuid();
            var slotResponse = new GetSlotQueryResponse { Id = slotId, Name = "TestName", LaneId = laneId };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetSlotQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slotResponse);

            // Act
            var result = await _controller.Get(slotId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(slotResponse);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenSlotDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetSlotQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GetSlotQueryResponse)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_ReturnsOk_WithSlots()
        {
            // Arrange
            var slotsResponse = new GetSlotsQueryResponse();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetSlotsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slotsResponse);

            // Act
            var result = await _controller.List();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(slotsResponse);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithSlotId()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var laneId = Guid.NewGuid();
            var slot = new Slot(slotId, laneId, "NewName");
            _mockMapper.Setup(m => m.Map<AddSlotCommand>(slot)).Returns(new AddSlotCommand { Id = slotId, Name = slot.Name, LaneId = laneId });
            var commandResponse = new AddSlotCommandResponse { Id = slotId, Name = slot.Name, LaneId = laneId };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddSlotCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Post(slot);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Value.Should().BeEquivalentTo(commandResponse);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var laneId = Guid.NewGuid();
            var slot = new Slot(slotId,laneId, "UpdatedName");
            _mockMapper.Setup(m => m.Map<UpdateSlotCommand>(slot)).Returns(new UpdateSlotCommand { Id = slot.Id, Name = slot.Name });
            var commandResponse = new UpdateSlotCommandResponse { Id = slot.Id, Name = slot.Name, LaneId = slot.LaneId };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateSlotCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResponse);

            // Act
            var result = await _controller.Put(slot);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<RemoveSlotCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.Delete(slotId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

