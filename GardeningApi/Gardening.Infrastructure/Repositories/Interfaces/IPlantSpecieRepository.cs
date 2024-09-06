using Gardening.Core.Entities;
using LanguageExt;

namespace Gardening.Infrastructure.Repositories.Interfaces
{
    public interface IPlantSpecieRepository
    {
        Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync();
        Task<Option<PlantSpecie>> GetPlantSpecieByIdAsync(int id);
        Task<PlantSpecie> CreatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<PlantSpecie?> UpdatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task DeletePlantSpecieAsync(int id);
    }
}
