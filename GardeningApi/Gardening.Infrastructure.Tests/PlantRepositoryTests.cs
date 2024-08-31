using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Tests
{
    public class PlantRepositoryTests : IDisposable
    {
        private readonly PlantAppDbContext _context;
        private readonly PlantRepository _plantRepository;

        public PlantRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PlantAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new PlantAppDbContext(options);
            _plantRepository = new PlantRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPlant()
        {
            // Arrange
            var plant = new Plant { Name = "Tomato", PlantingDate = DateTime.UtcNow,  PlantSpecie = new PlantSpecie() { Name = "Flower", Type = PlantType.Flower } };

            // Act
            await _plantRepository.CreatePlantAsync(plant);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Plants.FindAsync(plant.Id);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Tomato");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPlant_WhenExists()
        {
            // Arrange
            var plant = new Plant { Name = "Rosie", PlantingDate = DateTime.UtcNow, PlantSpecie = new PlantSpecie(){Name = "Rose", Type = PlantType.Tree}};
            await _context.Plants.AddAsync(plant);
            var saveChangesValue = await _context.SaveChangesAsync();

            // Act
            var result = await _plantRepository.GetPlantByIdAsync(plant.Id);

            // Assert
            saveChangesValue.Should().BeGreaterThan(0);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Rosie");
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
            var result = await _plantRepository.GetAllPlantsAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(plantList, options => options.Excluding(p => p.Id));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Act
            var result = await _plantRepository.GetPlantByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Update_ShouldModifyExistingPlant()
        {
            // Arrange
            var plant = new Plant { Name = "Lettuce", PlantingDate = DateTime.UtcNow, PlantSpecie = new PlantSpecie() { Name = "Lettuce", Type = PlantType.Vegetable } };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            // Act
            plant.Name = "Updated Lettuce";
            await _plantRepository.UpdatePlantAsync(plant);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Plants.FindAsync(plant.Id);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Updated Lettuce");
        }

        [Fact]
        public async Task Delete_ShouldRemovePlant_WhenExists()
        {
            // Arrange
            var plant = new Plant { Name = "Carrot", PlantingDate = DateTime.UtcNow, PlantSpecie = new PlantSpecie() { Name = "Carrot", Type = PlantType.Vegetable } };
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            // Act
            await _plantRepository.DeletePlantAsync(plant.Id);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.Plants.FindAsync(plant.Id);
            result.Should().BeNull();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
