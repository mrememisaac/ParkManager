using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Vehicles.Queries.GetVehicle;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class VehiclesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public async Task InitializeAsync()
        {
            var sp = _factory.Services.CreateScope().ServiceProvider;
            _factory.SetupDatabase(sp);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public VehiclesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/vehicles/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListVehicles_ReturnsSuccessStatusCode()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }

            _client = _factory.CreateClient();
            var response = await _client.GetAsync("");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_ReturnsNotFound_ForInvalidId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var response = await _client.GetAsync($"/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddVehicle_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var vehicle = new Models.Vehicle(Guid.NewGuid(), "Test", "Car", "123ABC");
            var response = await _client.PostAsJsonAsync("", vehicle);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdVehicle = await response.Content.ReadFromJsonAsync<GetVehicleQueryResponse>();
            Assert.NotNull(createdVehicle);
            Assert.Equal(vehicle.Registration, createdVehicle.Registration);
            Assert.Equal(vehicle.Model, createdVehicle.Model);
            Assert.Equal(vehicle.Make, createdVehicle.Make);
            Assert.Equal(vehicle.Id, createdVehicle.Id);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateVehicle_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var vehicle = new Models.Vehicle(Guid.NewGuid(), "Test", "Car", "123ABC");
            var response = await _client.PostAsJsonAsync("", vehicle);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a vehicle to update. This might require creating a vehicle first.
            var vehicleToUpdate = new { vehicle.Id, Make = "Updated", Model = "Car", Registration = "123ABC" };
            response = await _client.PutAsJsonAsync("", vehicleToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the vehicle was updated as expected
        }

        [Fact]
        public async Task DeleteVehicle_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var vehicle = new Models.Vehicle(Guid.NewGuid(), "Test", "Car", "123ABC");
            var response = await _client.PostAsJsonAsync("", vehicle);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{vehicle.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the vehicle was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
