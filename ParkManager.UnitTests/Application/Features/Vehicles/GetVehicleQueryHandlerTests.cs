using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetVehicleQueryHandlerTests
    {
        private readonly Mock<IVehiclesRepository> _mockVehiclesRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<GetVehicleQuery, GetVehicleQueryResponse> _handler;

        public GetVehicleQueryHandlerTests()
        {
            _mockVehiclesRepository = new Mock<IVehiclesRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetVehicleQueryHandler(_mockVehiclesRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnVehicle_WhenVehicleExists()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var expectedVehicle = new Vehicle(vehicleId, "Fiat", "CUV", "abc123");
            var expectedResponse = new GetVehicleQueryResponse { 
                Id = expectedVehicle.Id,
                Make = expectedVehicle.Make,
                Model = expectedVehicle.Model,
                Registration = expectedVehicle.Registration,                
            };
            _mockVehiclesRepository.Setup(repo => repo.Get(vehicleId)).ReturnsAsync(expectedVehicle);
            _mockMapper.Setup(m => m.Map<GetVehicleQueryResponse>(expectedVehicle)).Returns(new GetVehicleQueryResponse());
            var query = new GetVehicleQuery(vehicleId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _mockMapper.Verify(m => m.Map<GetVehicleQueryResponse>(expectedVehicle), Times.Once);
            _mockVehiclesRepository.Verify(repo => repo.Get(vehicleId), Times.Once);
            Assert.NotNull(result);
            
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenVehicleDoesNotExist()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            _mockVehiclesRepository.Setup(repo => repo.Get(vehicleId)).ReturnsAsync((Vehicle)null);
            var query = new GetVehicleQuery(vehicleId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

