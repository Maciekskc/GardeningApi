using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Gardening.Services.Services;
using Gardening.Services.Services.Interfaces;
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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

            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Should().NotBeNull();
                p.Id.Should().BeGreaterThan(0);
            });
        }

        [Fact]
        public async Task GetPlantByIdAsync_ShouldReturnPlant_WhenExists()
        {
            var plant = new Plant { Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecieId = 2 };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            var result = await _plantService.GetPlantByIdAsync(plant.Id);

            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Should().NotBeNull();
                p.Name.Should().Be("Rose");
            });
        }

        [Fact]
        public async Task GetAllPlantsAsync_ShouldReturnAllPlantsThatExists()
        {
            // Arrange
            var plantList = new List<Plant>()
            {
                new Plant { Name = "Rose", PlantingDate = DateTime.UtcNow, PlantSpecie = new PlantSpecie(){Name = "Rose", Type = PlantType.Flower}},
                new Plant { Name = "Violet", PlantingDate = DateTime.UtcNow, PlantSpecie = new PlantSpecie(){Name = "Violet", Type = PlantType.Flower}}
            };
            await _context.Plants.AddRangeAsync(plantList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _plantService.GetAllPlantsAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(plantList, options => options.Excluding(p => p.Id));
        }

        [Fact]
        public async Task GetPlantByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var result = await _plantService.GetPlantByIdAsync(999);

            result.IsFaulted.Should().BeTrue();
        }

        [Fact]
        public async Task UpdatePlantAsync_ShouldModifyExistingPlant()
        {
            var plant = new Plant { Name = "Lettuce", PlantingDate = DateTime.UtcNow, PlantSpecieId = 3 };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            plant.Name = "Updated Lettuce";
            var result = await _plantService.UpdatePlantAsync(plant.Id, plant);

            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Should().NotBeNull();
                p.Name.Should().Be("Updated Lettuce");
            });
        }

        [Theory]
        [InlineData(default, 1, "AnyName", PlantType.Flower, "Invalid id")]
        [InlineData(-1, 1, "AnyName", PlantType.Flower, "Invalid id")]
        [InlineData(2, 1, "AnyName", PlantType.Flower, "You cannot change record Id")]
        public async Task UpdatePlantSpecieAsync_ShouldReturnValidationError_WhenRequestObjectIsInvalid(int id, int updatedObjectId, string name, PlantType type, string expectedValidationErrorMessage)
        {
            var initialPlant = new Plant() { Id = 1, Name = "DefaultName", PlantSpecie = new PlantSpecie(){ Name = "Some name", Type = PlantType.Tree }, PlantingDate = DateTime.Now };
            await _context.Plants.AddAsync(initialPlant);
            await _context.SaveChangesAsync();

            var updatedPlant = new Plant { Id = updatedObjectId, Name = name, PlantSpecie = new PlantSpecie() { Name = "Some name", Type = type }, PlantingDate = DateTime.Now };
            var result = await _plantService.UpdatePlantAsync(id, updatedPlant);

            result.IsSuccess.Should().BeFalse();
            result.IfFail(p =>
            {
                p.Should().NotBeNull();
                p.Message.Should().Be(expectedValidationErrorMessage);
            });
        }

        [Fact]
        public async Task UpdatePlantSpecieAsync_ShouldReturnNotFoundException_WhenNotExist()
        {
            var plant = new Plant { Id = 1, Name = "Lettuce", PlantSpecieId = 1};

            var result = await _plantService.UpdatePlantAsync(plant.Id, plant);

            result.IsSuccess.Should().BeFalse();
            result.IfFail(p =>
            {
                p.Should().NotBeNull();
                p.Message.Should().Be("Plant not found in the database, nothing to update");
            });
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
