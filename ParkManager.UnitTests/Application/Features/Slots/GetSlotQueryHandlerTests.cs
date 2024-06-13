using MediatR;
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
        private readonly IRequestHandler<GetSlotQuery, Slot> _handler;

        public GetSlotQueryHandlerTests()
        {
            _mockSlotsRepository = new Mock<ISlotsRepository>();
            _handler = new GetSlotQueryHandler(_mockSlotsRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSlot_WhenSlotExists()
        {
            // Arrange
            var slotId = Guid.NewGuid();
            var expectedSlot = new Slot(slotId, Guid.NewGuid(), "A1");
            _mockSlotsRepository.Setup(repo => repo.Get(slotId)).ReturnsAsync(expectedSlot);
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
