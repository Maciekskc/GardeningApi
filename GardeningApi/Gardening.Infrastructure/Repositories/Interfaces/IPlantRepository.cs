using Gardening.Core.Entities;
using LanguageExt;

namespace Gardening.Infrastructure.Repositories.Interfaces
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
        Task<Option<Plant>> GetPlantByIdAsync(int id);
        Task<Option<Plant>> CreatePlantAsync(Plant plant);
        Task<Option<Plant>> UpdatePlantAsync(Plant plant);
        Task<Option<int>> DeletePlantAsync(Plant plant);
    }
}