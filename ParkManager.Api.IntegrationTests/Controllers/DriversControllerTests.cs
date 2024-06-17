using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Drivers.Queries.GetDriver;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class DriversControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public DriversControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/drivers/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListDrivers_ReturnsSuccessStatusCode()
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
        public async Task GetDriver_ReturnsNotFound_ForInvalidId()
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
        public async Task AddDriver_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var driver = new Models.Driver(Guid.NewGuid(), "Zack Zill", "08012345678");
            var response = await _client.PostAsJsonAsync("", driver);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdDriver = await response.Content.ReadFromJsonAsync<GetDriverQueryResponse>();
            Assert.NotNull(createdDriver);
            Assert.Equal(driver.Name, createdDriver.Name);
            Assert.Equal(driver.PhoneNumber, createdDriver.PhoneNumber);
            Assert.Equal(driver.Id, createdDriver.Id);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateDriver_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var driver = new Models.Driver(Guid.NewGuid(), "Zack Zill", "08012345678");
            var response = await _client.PostAsJsonAsync("", driver);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a driver to update. This might require creating a driver first.
            var driverToUpdate = new { driver.Id, Name = "Updated", PhoneNumber = "08012345679" };
            response = await _client.PutAsJsonAsync("", driverToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the driver was updated as expected
        }

        [Fact]
        public async Task DeleteDriver_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var driver = new Models.Driver(Guid.NewGuid(), "Zack Zill", "08012345678");
            var response = await _client.PostAsJsonAsync("", driver);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{driver.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the driver was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
