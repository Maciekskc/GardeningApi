﻿using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;
using Gardening.Services.Services.Interfaces;
using GardeningApi.Controllers;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GardeningApi.Tests
{
    public class PlantSpecieControllerTests
    {
        private readonly Mock<IPlantSpecieService> _plantSpecieServiceMock;
        private readonly PlantSpecieController _plantSpecieController;

        public PlantSpecieControllerTests()
        {
            _plantSpecieServiceMock = new Mock<IPlantSpecieService>();
            _plantSpecieController = new PlantSpecieController(_plantSpecieServiceMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnAllPlantSpecies()
        {
            var plantSpecies = new List<PlantSpecie>
            {
                new PlantSpecie { Id = 1, Name = "Rose", Type = PlantType.Flower },
                new PlantSpecie { Id = 2, Name = "Tomato", Type = PlantType.Vegetable }
            };

            _plantSpecieServiceMock.Setup(service => service.GetAllPlantSpeciesAsync()).ReturnsAsync(plantSpecies);

            var result = await _plantSpecieController.Get();

            result.Should().BeEquivalentTo(plantSpecies);
        }

        [Fact]
        public async Task Get_ShouldReturnPlantSpecieById()
        {
            var plantSpecie = new PlantSpecie { Id = 1, Name = "Rose", Type = PlantType.Flower };

            _plantSpecieServiceMock.Setup(service => service.GetPlantSpecieByIdAsync(1))
                .ReturnsAsync(new Result<PlantSpecie>(plantSpecie));

            var result = await _plantSpecieController.Get(1);

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(plantSpecie);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenPlantDoesNotExist()
        {
            var result = await _plantSpecieController.Get(-1);


            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Post_ShouldCreatePlantSpecie()
        {
            var plantSpecie = new PlantSpecie { Id = 1, Name = "Rose", Type = PlantType.Flower };

            _plantSpecieServiceMock.Setup(service => service.CreatePlantSpecieAsync(plantSpecie))
                .ReturnsAsync(plantSpecie);

            var result = await _plantSpecieController.Post(plantSpecie);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Put_ShouldUpdatePlantSpecie()
        {
            var plantSpecie = new PlantSpecie { Id = 1, Name = "Updated Rose", Type = PlantType.Flower };

            _plantSpecieServiceMock.Setup(service => service.UpdatePlantSpecieAsync(plantSpecie.Id, plantSpecie))
                .ReturnsAsync(new Result<PlantSpecie>(plantSpecie));

            var result = await _plantSpecieController.Put(1, plantSpecie);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Put_ShouldThrowBadRequest_WhenUpdatingPlantSpecieWithDifferentIdThanParameter()
        {
            var plantSpacieId = 0;
            var plantSpacie = new PlantSpecie { Id = 1, Name = "Updated Rose", Type = PlantType.Flower };

            var result = await _plantSpecieController.Put(plantSpacieId, plantSpacie);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ShouldRemovePlantSpecie()
        {
            _plantSpecieServiceMock.Setup(service => service.DeletePlantSpecieAsync(1))
                .Returns(Task.FromResult(new Result<int>(1)));

            var result = await _plantSpecieController.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}