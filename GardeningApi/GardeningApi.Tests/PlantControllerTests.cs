﻿using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using GardeningApi.Controllers;
using LanguageExt.Common;
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

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(plant);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenPlantDoesNotExist()
        {
            var result = await _plantController.Get(-1);

            result.Value.Should().Be(null);
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Post_ShouldCreatePlant()
        {
            var plant = new Plant { Id = 1, Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            _plantServiceMock.Setup(service => service.CreatePlantAsync(plant)).ReturnsAsync(plant);

            var result = await _plantController.Post(plant);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Put_ShouldUpdatePlant()
        {
            var plant = new Plant { Id = 1, Name = "Updated Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            _plantServiceMock.Setup(service => service.UpdatePlantAsync(plant.Id, plant))
                .ReturnsAsync(new Result<Plant>(plant));

            var result = await _plantController.Put(1, plant);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Put_ShouldThrowBadRequest_WhenUpdatingPlantWithDifferentIdThanParameter()
        {
            var plantId = 0;
            var plant = new Plant { Id = 1, Name = "Updated Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            var result = await _plantController.Put(plantId, plant);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ShouldRemovePlant()
        {
            _plantServiceMock.Setup(service => service.DeletePlantAsync(1))
                .Returns(Task.FromResult(new Result<int>(1)));

            var result = await _plantController.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}