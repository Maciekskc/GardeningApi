using Gardening.Core.Entities;

namespace Gardening.Services.Services.Interfaces
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
        Task<Plant> GetPlantByIdAsync(int id);
        Task<Plant> CreatePlantAsync(Plant plant);
        Task<Plant> UpdatePlantAsync(Plant plant);
        Task DeletePlantAsync(int id);
    }
}