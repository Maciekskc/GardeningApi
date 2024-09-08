using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Tests
{
    public class PlantSpecieRepositoryTests : IDisposable
    {
        private readonly PlantAppDbContext _context;
        private readonly PlantSpecieRepository _plantSpecieRepository;

        public PlantSpecieRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PlantAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new PlantAppDbContext(options);
            _plantSpecieRepository = new PlantSpecieRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPlantSpecie()
        {
            // Arrange
            var plantSpecie = new PlantSpecie { Name = "Tomato", Type = PlantType.Vegetable };

            // Act
            await _plantSpecieRepository.CreatePlantSpecieAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.PlantSpecies.FindAsync(plantSpecie.Id);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Tomato");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPlantSpecie_WhenExists()
        {
            // Arrange
            var plantSpecie = new PlantSpecie { Name = "Rose", Type = PlantType.Flower };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _plantSpecieRepository.GetPlantSpecieByIdAsync(plantSpecie.Id);
            
            // Assert
            result.IsSome.Should().BeTrue();
            result.IfSome(p =>
            {
                p.Should().NotBeNull();
                p.Name.Should().Be("Rose");
            });
        }

        [Fact]
        public async Task GetAllPlantSpeciesAsync_ShouldReturnAllPlantsThatExists()
        {
            // Arrange
            var plantSpacieList = new List<PlantSpecie>()
            {
                new PlantSpecie { Name = "Mint", Type = PlantType.Herb },
                new PlantSpecie { Name = "Paper Mint", Type = PlantType.Herb }
            };
            await _context.PlantSpecies.AddRangeAsync(plantSpacieList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _plantSpecieRepository.GetAllPlantSpeciesAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(plantSpacieList, options => options.Excluding(p => p.Id));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Act
            var result = await _plantSpecieRepository.GetPlantSpecieByIdAsync(999);

            // Assert
            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task Update_ShouldModifyExistingPlantSpecie()
        {
            // Arrange
            var plantSpecie = new PlantSpecie { Name = "Lettuce", Type = PlantType.Vegetable };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Act
            plantSpecie.Name = "Updated Lettuce";
            await _plantSpecieRepository.UpdatePlantSpecieAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _context.PlantSpecies.FindAsync(plantSpecie.Id);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Updated Lettuce");
        }

        [Fact]
        public async Task Delete_ShouldRemovePlantSpecie_WhenExists()
        {
            // Arrange
            var plantSpecie = new PlantSpecie { Name = "Carrot", Type = PlantType.Vegetable };
            await _context.PlantSpecies.AddAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.PlantSpecies.FindAsync(plantSpecie.Id);
            result.Should().Be(plantSpecie);

            _context.Entry(plantSpecie).State = EntityState.Modified;
            await _plantSpecieRepository.DeletePlantSpecieAsync(plantSpecie);
            await _context.SaveChangesAsync();

            // Assert
            result = await _context.PlantSpecies.FindAsync(plantSpecie.Id);
            result.Should().Be(null);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
