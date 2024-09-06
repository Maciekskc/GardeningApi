using Gardening.Core.Entities;
using LanguageExt;
using LanguageExt.Common;

namespace Gardening.Services.Services.Interfaces
{
    public interface IPlantSpecieService
    {
        Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync();
        Task<Result<PlantSpecie>> GetPlantSpecieByIdAsync(int id);
        Task<PlantSpecie> CreatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<Option<PlantSpecie>> UpdatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task DeletePlantSpecieAsync(int id);
    }
}
