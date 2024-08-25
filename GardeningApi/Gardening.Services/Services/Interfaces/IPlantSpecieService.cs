using Gardening.Core.Entities;

namespace Gardening.Services.Services.Interfaces
{
    public interface IPlantSpecieService
    {
        Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync();
        Task<PlantSpecie> GetPlantSpecieByIdAsync(int id);
        Task<PlantSpecie> CreatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<PlantSpecie> UpdatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task DeletePlantSpecieAsync(int id);
    }
}
