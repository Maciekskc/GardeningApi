using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Gardening.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Services.Tests
{
    public class PlantSpecieServiceTests : IDisposable
    {
        private readonly PlantAppDbContext _context;
        private readonly PlantSpecieService _plantSpecieService;

        public PlantSpecieServiceTests()
        {
            var options = new DbContextOptionsBuilder<PlantAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new PlantAppDbContext(options);
            var repository = new PlantSpecieRepository(_context);
            _plantSpecieService = new PlantSpecieService(repository);
        }

        [Fact]
        public async Task CreatePlantSpecieAsync_ShouldAddNewPlantSpecie()
        {
            var plantSpecie = new PlantSpecie { Name = "Tomato", Type = PlantType.Vegetable };

            var result = await _plantSpecieService.CreatePlantSpecieAsync(plantSpecie);
            
            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Id.Should().BeGreaterThan(0);
                p.Name.Should().Be("Tomato");
            });
        }

        [Fact]
        public async Task GetPlantSpecieByIdAsync_ShouldReturnPlantSpecie_WhenExists()
        {
            var plantSpecie = new PlantSpecie { Name = "Rose", Type = PlantType.Flower };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            var result = await _plantSpecieService.GetPlantSpecieByIdAsync(plantSpecie.Id);

            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Should().NotBeNull();
                p.Name.Should().Be("Rose");
            });
        }

        [Fact]
        public async Task GetPlantSpecieByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var result = await _plantSpecieService.GetPlantSpecieByIdAsync(999);

            result.IsFaulted.Should().BeTrue();
            result.IfFail(f =>
            {
                f.Message.Should().Be("The object does not exist");
            });
        }

        [Fact]
        public async Task GetAllPlantSpeciesAsync_ShouldReturnAllPlantsThatExists()
        {
            // Arrange
            var plantSpacieList = new List<PlantSpecie>()
            {
                new() { Name = "Mint", Type = PlantType.Herb },
                new() { Name = "Paper Mint", Type = PlantType.Herb }
            };
            await _context.PlantSpecies.AddRangeAsync(plantSpacieList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _plantSpecieService.GetAllPlantSpeciesAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(plantSpacieList, options => options.Excluding(p => p.Id));
        }

        [Fact]
        public async Task UpdatePlantSpecieAsync_ShouldModifyExistingPlantSpecie()
        {
            var plantSpecie = new PlantSpecie { Name = "Lettuce", Type = PlantType.Vegetable };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            plantSpecie.Name = "Updated Lettuce";
            var result = await _plantSpecieService.UpdatePlantSpecieAsync(plantSpecie.Id,plantSpecie);

            result.IsSuccess.Should().BeTrue();
            result.IfSucc(p =>
            {
                p.Should().NotBeNull();
                p.Name.Should().Be("Updated Lettuce");
            });
        }

        [Theory]
        [InlineData(default,1, "AnyName", PlantType.Flower, "Invalid id")]
        [InlineData(-1,1, "AnyName", PlantType.Flower, "Invalid id")]
        [InlineData(2,1, "AnyName", PlantType.Flower, "You cannot change record Id")]
        public async Task UpdatePlantSpecieAsync_ShouldReturnValidationError_WhenRequestObjectIsInvalid(int id, int updatedObjectId, string name, PlantType type, string expectedValidationErrorMessage)
        {
            var initialPlantSpecie = new PlantSpecie { Id = 1, Name = "DefaultName", Type = PlantType.Tree};
            await _context.PlantSpecies.AddAsync(initialPlantSpecie);
            await _context.SaveChangesAsync();

            var updatedPlantSpecie = new PlantSpecie { Id = updatedObjectId, Name = name, Type = type};
            var result = await _plantSpecieService.UpdatePlantSpecieAsync(id, updatedPlantSpecie);

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
            var plantSpecie = new PlantSpecie { Id = 1, Name = "Lettuce", Type = PlantType.Vegetable };

            var result = await _plantSpecieService.UpdatePlantSpecieAsync(plantSpecie.Id, plantSpecie);

            result.IsSuccess.Should().BeFalse();
            result.IfFail(p =>
            {
                p.Should().NotBeNull();
                p.Message.Should().Be("Plant Specie not found in the database, nothing to update");
            });
        }

        [Fact]
        public async Task DeletePlantSpecieAsync_ShouldRemovePlantSpecie_WhenExists()
        {
            var plantSpecie = new PlantSpecie { Name = "Carrot", Type = PlantType.Vegetable };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            await _plantSpecieService.DeletePlantSpecieAsync(plantSpecie.Id);

            var result = await _context.PlantSpecies.FindAsync(plantSpecie.Id);
            result.Should().BeNull();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
