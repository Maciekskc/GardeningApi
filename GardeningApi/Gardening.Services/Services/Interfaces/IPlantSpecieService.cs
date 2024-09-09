using Gardening.Core.Entities;
using LanguageExt.Common;

namespace Gardening.Services.Services.Interfaces
{
    public interface IPlantSpecieService
    {
        Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync();
        Task<Result<PlantSpecie>> GetPlantSpecieByIdAsync(int id);
        Task<Result<PlantSpecie>> CreatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<Result<PlantSpecie>> UpdatePlantSpecieAsync(int id, PlantSpecie plantSpecie);
        Task<Result<int>> DeletePlantSpecieAsync(int id);
    }
}
