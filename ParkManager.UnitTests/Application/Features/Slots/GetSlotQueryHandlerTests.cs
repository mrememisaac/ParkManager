using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Slots.Queries.GetSlot;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetSlotQueryHandlerTests
    {
        private readonly Mock<ISlotsRepository> _mockSlotsRepository;
        private readonly IRequestHandler<GetSlotQuery, GetSlotQueryResponse> _handler;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<GetSlotQueryHandler>> _mockLogger;

        public GetSlotQueryHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetSlotQueryHandler>>();
            _handler = new GetSlotQueryHandler(_mockSlotsRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSlot_WhenSlotExists()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var expectedSlot = new Slot(slotId, Guid.NewGuid(), "A1");
            var expectedResponse = new GetSlotQueryResponse() { Id = expectedSlot.Id, Name = expectedSlot.Name };
            _mockSlotsRepository.Setup(repo => repo.Get(slotId)).ReturnsAsync(expectedSlot);
            _mockMapper.Setup(m => m.Map<GetSlotQueryResponse>(expectedSlot)).Returns(expectedResponse);
            var query = new GetSlotQuery (slotId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSlot.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenSlotDoesNotExist()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            _mockSlotsRepository.Setup(repo => repo.Get(slotId)).ReturnsAsync((Slot)null);
            var query = new GetSlotQuery(slotId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
