using Gardening.Core.Entities;
using Gardening.Services.DTOs.Plant.Post;
using Riok.Mapperly.Abstractions;

namespace Gardening.Services.Mappings
{
    [Mapper]
    public static partial class PlantMapper
    {
        public static partial Plant PostPlantRequestToPlant(PostPlantRequest dto);

        [MapProperty([nameof(Plant.PlantSpecie), nameof(Plant.PlantSpecie.Name)], [nameof(PostPlantResponse.Specie)])]
        public static partial PostPlantResponse PlantToPostPlantResponse(Plant dto);
    }
}