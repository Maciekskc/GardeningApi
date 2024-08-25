using Gardening.Core.Entities;

namespace Gardening.Infrastructure.Repositories.Interfaces
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
        Task<Plant> GetPlantByIdAsync(int id);
        Task<Plant> CreatePlantAsync(Plant plant);
        Task<Plant> UpdatePlantAsync(Plant plant);
        Task DeletePlantAsync(int id);
    }
}