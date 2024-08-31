using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;

namespace Gardening.Core.Tests
{
    public class PlantTests
    {
        [Fact]
        public void Plant_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = 1;
            var name = "Rose";
            var plantingDate = DateTime.UtcNow;
            var plantSpecieId = 1;
            var plantSpecie = new PlantSpecie { Id = plantSpecieId, Name = "Carrot", Type = PlantType.Vegetable };

            // Act
            var plant = new Plant
            {
                Id = id,
                Name = name,
                PlantingDate = plantingDate,
                PlantSpecieId = plantSpecieId,
                PlantSpecie = plantSpecie
            };

            // Assert
            plant.Id.Should().Be(id);
            plant.Name.Should().Be(name);
            plant.PlantingDate.Should().Be(plantingDate);
            plant.PlantSpecieId.Should().Be(plantSpecieId);
            plant.PlantSpecie.Should().Be(plantSpecie);
        }

        [Fact]
        public void Plant_DefaultConstructor_ShouldInitializeProperties()
        {
            // Act
            var plant = new Plant();

            // Assert
            plant.Id.Should().Be(default);
            plant.Name.Should().Be("Plant");
            plant.PlantingDate.Should().Be(default);
            plant.PlantSpecieId.Should().BeNull();
            plant.PlantSpecie.Should().BeNull();
        }

        [Fact]
        public void Plant_SetAndGetProperties_ShouldWorkCorrectly()
        {
            // Arrange
            var plant = new Plant();

            // Act
            plant.Id = 1;
            plant.Name = "Rose";
            plant.PlantingDate = DateTime.UtcNow;
            plant.PlantSpecieId = 1;

            // Assert
            plant.Id.Should().Be(1);
            plant.Name.Should().Be("Rose");
            plant.PlantingDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            plant.PlantSpecieId.Should().Be(1);
        }
    }
}