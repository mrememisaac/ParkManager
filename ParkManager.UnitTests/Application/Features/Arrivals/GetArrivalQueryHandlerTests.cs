using AutoMapper;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetArrivalQueryHandlerTests
    {
        private readonly Mock<IArrivalsRepository> _mockArrivalsRepository;
        private readonly IRequestHandler<GetArrivalQuery, GetArrivalQueryResponse> _handler;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<GetArrivalQueryHandler>> _mockLogger;

        public GetArrivalQueryHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _mockArrivalsRepository = new Mock<IArrivalsRepository>();
            _mockLogger = new Mock<ILogger<GetArrivalQueryHandler>>();
            _handler = new GetArrivalQueryHandler(_mockArrivalsRepository.Object, _mapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectArrival_WhenIdIsValid()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var expectedArrival = new Arrival(validId, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());            
            _mockArrivalsRepository.Setup(repo => repo.Get(validId)).ReturnsAsync(expectedArrival);
            var expectedResponse = new GetArrivalQueryResponse
            {
                Id = validId,
                Timestamp = expectedArrival.Timestamp,
                DriverId = expectedArrival.DriverId,
                ParkId = expectedArrival.ParkId,
                VehicleId = expectedArrival.VehicleId,
                TagId = expectedArrival.TagId
            };
            _mapper.Setup(m => m.Map<GetArrivalQueryResponse>(expectedArrival)).Returns(expectedResponse);
            var query = new GetArrivalQuery(validId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenArrivalDoesNotExist()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _mockArrivalsRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((Arrival)null);
            var query = new GetArrivalQuery(invalidId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
