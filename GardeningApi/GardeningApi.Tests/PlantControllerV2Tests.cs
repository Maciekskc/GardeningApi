using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Services.DTOs.Plant.Post;
using Gardening.Services.Services.Interfaces;
using GardeningApi.Controllers._2._0;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GardeningApi.Tests
{
    public class PlantControllerV2Tests
    {
        private readonly Mock<IPlantService> _plantServiceMock;
        private readonly PlantController _plantController;

        public PlantControllerV2Tests()
        {
            _plantServiceMock = new Mock<IPlantService>();
            _plantController = new PlantController(_plantServiceMock.Object);
        }

        
        [Fact]
        public async Task Post_ShouldCreatePlant()
        {
            var plantRequest = new PostPlantRequest() { Name = "Rose", Specie = "Mint"};
            var plantResponse = new PostPlantResponse() { Name = "Rose", PlantingDate = DateTime.UtcNow, Specie = "Mint"};

            _plantServiceMock.Setup(service => service.CreatePlantAsync(plantRequest)).ReturnsAsync(plantResponse);

            var result = await _plantController.Post(plantRequest);

            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(plantResponse);
        }
    }
}