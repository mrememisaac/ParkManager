using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Occasions.Queries.GetOccasion;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class OccasionsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public OccasionsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/occasions/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListOccasions_ReturnsSuccessStatusCode()
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
        public async Task GetOccasion_ReturnsNotFound_ForInvalidId()
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
        public async Task AddOccasion_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var occasion = new Models.Occasion(Guid.NewGuid(), "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            var response = await _client.PostAsJsonAsync("", occasion);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdOccasion = await response.Content.ReadFromJsonAsync<GetOccasionQueryResponse>();
            Assert.NotNull(createdOccasion);
            Assert.Equal(occasion.Name, createdOccasion.Name);
            Assert.Equal(occasion.Id, createdOccasion.Id);
            Assert.Equal(occasion.StartDate, createdOccasion.StartDate);
            Assert.Equal(occasion.EndDate, createdOccasion.EndDate);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateOccasion_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var occasion = new Models.Occasion(Guid.NewGuid(), "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            var response = await _client.PostAsJsonAsync("", occasion);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a occasion to update. This might require creating a occasion first.
            var occasionToUpdate = new Models.Occasion(occasion.Id, "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            response = await _client.PutAsJsonAsync("", occasionToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the occasion was updated as expected
        }

        [Fact]
        public async Task DeleteOccasion_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var occasion = new Models.Occasion(Guid.NewGuid(), "Occasion 1", DateTime.Now, DateTime.Now.AddDays(1));
            var response = await _client.PostAsJsonAsync("", occasion);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{occasion.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the occasion was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
