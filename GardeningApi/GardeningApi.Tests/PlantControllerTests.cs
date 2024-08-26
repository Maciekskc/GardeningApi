using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using GardeningApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GardeningApi.Tests
{
    public class PlantControllerTests
    {
        private readonly Mock<IPlantService> _plantServiceMock;
        private readonly PlantController _plantController;

        public PlantControllerTests()
        {
            _plantServiceMock = new Mock<IPlantService>();
            _plantController = new PlantController(_plantServiceMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnAllPlants()
        {
            var plants = new List<Plant>
            {
                new Plant { Id = 1, Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 },
                new Plant { Id = 2, Name = "Tomato", PlantingDate = DateTime.UtcNow, PlantSpecieId = 2 }
            };

            _plantServiceMock.Setup(service => service.GetAllPlantsAsync()).ReturnsAsync(plants);

            var result = await _plantController.Get();

            result.Should().BeEquivalentTo(plants);
        }

        [Fact]
        public async Task Get_ShouldReturnPlantById()
        {
            var plant = new Plant { Id = 1, Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            _plantServiceMock.Setup(service => service.GetPlantByIdAsync(1)).ReturnsAsync(plant);

            var result = await _plantController.Get(1);

            result.Value.Should().Be(plant);
        }

        [Fact]
        public async Task Post_ShouldCreatePlant()
        {
            var plant = new Plant { Id = 1, Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            _plantServiceMock.Setup(service => service.CreatePlantAsync(plant)).ReturnsAsync(plant);

            var result = await _plantController.Post(plant);

            result.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Put_ShouldUpdatePlant()
        {
            var plant = new Plant { Id = 1, Name = "Updated Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            _plantServiceMock.Setup(service => service.UpdatePlantAsync(plant)).ReturnsAsync(plant);

            var result = await _plantController.Put(1, plant);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ShouldRemovePlant()
        {
            _plantServiceMock.Setup(service => service.DeletePlantAsync(1)).Returns(Task.CompletedTask);

            var result = await _plantController.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
