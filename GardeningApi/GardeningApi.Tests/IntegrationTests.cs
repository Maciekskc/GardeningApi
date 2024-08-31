using FluentAssertions;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GardeningApi.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var url = "/api/Plant"; // Assuming you have an endpoint for plants

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status code 200-299
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public void Services_ShouldBeRegisteredCorrectly()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>();

            // Act
            var scope = factory.Services.CreateScope();
            var plantService = scope.ServiceProvider.GetService<IPlantService>();

            // Assert
            plantService.Should().NotBeNull();
        }
    }
}
