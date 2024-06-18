using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Departures.Queries.GetDeparture;
using ParkManager.Domain;

namespace ParkManager.UnitTests
{
    public class GetDepartureQueryHandlerTests
    {
        private readonly Mock<IDeparturesRepository> _mockDeparturesRepository;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<GetDepartureQueryHandler>> _mockLogger;
        private readonly IRequestHandler<GetDepartureQuery, GetDepartureQueryResponse> _handler;

        public GetDepartureQueryHandlerTests()
        {
            _mockDeparturesRepository = new Mock<IDeparturesRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetDepartureQueryHandler>>();
            _handler = new GetDepartureQueryHandler(_mockDeparturesRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectDeparture_WhenIdIsValid()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var expectedDeparture = new Departure(validId, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var expectedResponse = new GetDepartureQueryResponse
            {
                Id = expectedDeparture.Id,
                DriverId = expectedDeparture.DriverId,
                VehicleId = expectedDeparture.VehicleId,
                ParkId = expectedDeparture.ParkId
            };
            _mockDeparturesRepository.Setup(repo => repo.Get(validId)).ReturnsAsync(expectedDeparture);
            _mockMapper.Setup(m => m.Map<GetDepartureQueryResponse>(expectedDeparture)).Returns(expectedResponse);
            var query = new GetDepartureQuery(validId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenDepartureDoesNotExist()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            _mockDeparturesRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((Departure)null);
            var query = new GetDepartureQuery(invalidId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
