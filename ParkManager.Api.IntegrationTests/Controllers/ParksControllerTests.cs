using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Parks.Queries.GetPark;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class ParksControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public ParksControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/parks/");
            _factory = factory;
        }

        [Fact]
        public async Task ListParks_ReturnsSuccessStatusCode()
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
        public async Task GetPark_ReturnsNotFound_ForInvalidId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var response = await _client.GetAsync($"{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddPark_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var park = new Models.Park(Guid.NewGuid(), "Park 1", "Street 1", "City 1", "State 1", "Country 1", 1.1, 2.2);
            var response = await _client.PostAsJsonAsync("", park);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdPark = await response.Content.ReadFromJsonAsync<GetParkQueryResponse>();
            Assert.NotNull(createdPark);            
            Assert.Equal(park.Id, createdPark.Id);
            Assert.Equal(park.Name, createdPark.Name);
            Assert.Equal(park.Street, createdPark.Street);
            Assert.Equal(park.City, createdPark.City);
            Assert.Equal(park.State, createdPark.State);
            Assert.Equal(park.Country, createdPark.Country);
            Assert.Equal(park.Latitude, createdPark.Latitude);
            Assert.Equal(park.Longitude, createdPark.Longitude);
        }

        [Fact]
        public async Task UpdatePark_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var park = new Models.Park(Guid.NewGuid(), "Park 1", "Street 1", "City 1", "State 1", "Country 1", 1.1, 2.2);
            var response = await _client.PostAsJsonAsync("", park);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a park to update. This might require creating a park first.
            var parkToUpdate = new Models.Park(park.Id, "Park 1", "Street 1", "City 1", "State 1", "Country 1", 1.1, 2.2);
            response = await _client.PutAsJsonAsync("", parkToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the park was updated as expected
        }

        [Fact]
        public async Task DeletePark_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var park = new Models.Park(Guid.NewGuid(), "Park 1", "Street 1", "City 1", "State 1", "Country 1", 1.1, 2.2);
            var response = await _client.PostAsJsonAsync("", park);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{park.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the park was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
