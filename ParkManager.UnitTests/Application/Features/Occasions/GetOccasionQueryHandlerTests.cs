using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetOccasionQueryHandlerTests
    {
        private readonly Mock<IOccasionsRepository> _mockOccasionsRepository;
        private readonly IRequestHandler<GetOccasionQuery, GetOccasionQueryResponse> _handler;
        private readonly Mock<IMapper> _mockMapper; 
        private readonly Mock<ILogger<GetOccasionQueryHandler>> _mockLogger;

        public GetOccasionQueryHandlerTests()
        {
            _mockOccasionsRepository = new Mock<IOccasionsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetOccasionQueryHandler>>();
            _handler = new GetOccasionQueryHandler(_mockOccasionsRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOccasion_WhenOccasionExists()
        {
            // Arrange
            var occasionId = Guid.NewGuid();
            var expectedOccasion = new Occasion(occasionId, "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            var expectedResponse = new GetOccasionQueryResponse
            {
                Id = expectedOccasion.Id,
                EndDate = expectedOccasion.EndDate,
                Name = expectedOccasion.Name,
                StartDate = expectedOccasion.StartDate
            };
            _mockOccasionsRepository.Setup(repo => repo.Get(occasionId)).ReturnsAsync(expectedOccasion);
            _mockMapper.Setup(m => m.Map<GetOccasionQueryResponse>(expectedOccasion)).Returns(expectedResponse);
            var query = new GetOccasionQuery(occasionId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOccasion.Id, result.Id);
        }
    }
}
