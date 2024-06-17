using System.Net;
using ParkManager.Api.IntegrationTests.Base;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Api.IntegrationTests.Controllers
{
    public class TagsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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

        public TagsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/tags/");
            _factory = factory;
            //_client = factory.CreateClient();
        }

        [Fact]
        public async Task ListTags_ReturnsSuccessStatusCode()
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
        public async Task GetTag_ReturnsNotFound_ForInvalidId()
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
        public async Task AddTag_ReturnsCreatedResponse()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var tag = new Models.Tag(Guid.NewGuid(), 1);
            var response = await _client.PostAsJsonAsync("", tag);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdTag = await response.Content.ReadFromJsonAsync<GetTagQueryResponse>();
            Assert.NotNull(createdTag);
            Assert.Equal(tag.Number, createdTag.Number);
            Assert.Equal(tag.Id, createdTag.Id);
            // Additional assertions as necessary
        }

        [Fact]
        public async Task UpdateTag_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();

            var tag = new Models.Tag(Guid.NewGuid(), 1);
            var response = await _client.PostAsJsonAsync("", tag);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Assuming you have a tag to update. This might require creating a tag first.
            var tagToUpdate = new Models.Tag(tag.Id, 2);
            response = await _client.PutAsJsonAsync("", tagToUpdate);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the tag was updated as expected
        }

        [Fact]
        public async Task DeleteTag_ReturnsNoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ParkManagerDbContext>();
                context.Database.EnsureCreated();
            }
            _client = _factory.CreateClient();
            var tag = new Models.Tag(Guid.NewGuid(), 1);
            var response = await _client.PostAsJsonAsync("", tag);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.DeleteAsync($"{tag.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            // Additional validation to ensure the tag was deleted as expected
        }

        // Additional tests for other scenarios, including failure cases and validating response content
    }
}
