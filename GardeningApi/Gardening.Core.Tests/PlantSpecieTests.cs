using FluentAssertions;
using Gardening.Core.Entities;
using Gardening.Core.Enums;

namespace Gardening.Core.Tests
{
    public class PlantSpecieTests
    {
        [Fact]
        public void PlantSpecie_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = 1;
            var name = "Rose";
            var type = PlantType.Flower;

            // Act
            var plantSpecie = new PlantSpecie
            {
                Id = id,
                Name = name,
                Type = type
            };

            // Assert
            plantSpecie.Id.Should().Be(id);
            plantSpecie.Name.Should().Be(name);
            plantSpecie.Type.Should().Be(type);
        }

        [Fact]
        public void PlantSpecie_SetAndGetProperties_ShouldWorkCorrectly()
        {
            // Arrange
            var plantSpecie = new PlantSpecie();

            // Act
            plantSpecie.Id = 1;
            plantSpecie.Name = "Rose";
            plantSpecie.Type = PlantType.Flower;

            // Assert
            plantSpecie.Id.Should().Be(1);
            plantSpecie.Name.Should().Be("Rose");
            plantSpecie.Type.Should().Be(PlantType.Flower);
        }
    }
}