﻿using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Gardening.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Services.Tests
{
    public class PlantServiceTests
    {
        private readonly PlantService _plantService;
        private readonly PlantAppDbContext _context;

        public PlantServiceTests()
        {
            var options = new DbContextOptionsBuilder<PlantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "PlantDb_Test")
                .Options;

            _context = new PlantAppDbContext(options);
            var repository = new PlantRepository(_context);
            _plantService = new PlantService(repository);
        }

        [Fact]
        public async Task CreatePlantAsync_ShouldAddNewPlant()
        {
            var plant = new Plant { Name = "Tomato", PlantingDate = DateTime.UtcNow, PlantSpecieId = 1 };

            var result = await _plantService.CreatePlantAsync(plant);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be("Tomato");
        }

        [Fact]
        public async Task GetPlantByIdAsync_ShouldReturnPlant_WhenExists()
        {
            var plant = new Plant { Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 2 };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            var result = await _plantService.GetPlantByIdAsync(plant.Id);

            result.Should().NotBeNull();
            result.Name.Should().Be("Rose");
        }

        [Fact]
        public async Task GetPlantByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var result = await _plantService.GetPlantByIdAsync(999);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdatePlantAsync_ShouldModifyExistingPlant()
        {
            var plant = new Plant { Name = "Lettuce", PlantingDate = DateTime.UtcNow, PlantSpecieId = 3 };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            plant.Name = "Updated Lettuce";
            var result = await _plantService.UpdatePlantAsync(plant);

            result.Should().NotBeNull();
            result.Name.Should().Be("Updated Lettuce");
        }

        [Fact]
        public async Task DeletePlantAsync_ShouldRemovePlant_WhenExists()
        {
            var plant = new Plant { Name = "Carrot", PlantingDate = DateTime.UtcNow, PlantSpecieId = 4 };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            await _plantService.DeletePlantAsync(plant.Id);

            var result = await _context.Plants.FindAsync(plant.Id);
            result.Should().BeNull();
        }
    }
}