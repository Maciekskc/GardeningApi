using System.Net.Mime;
using Gardening.Core.Entities;
using Gardening.Infrastructure.Repositories.Interfaces;
using Gardening.Services.Services.Interfaces;

namespace Gardening.Services.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _repository;

        public PlantService(IPlantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            return await _repository.GetAllPlantsAsync();
        }

        public async Task<Plant?> GetPlantByIdAsync(int id)
        {
            return await _repository.GetPlantByIdAsync(id);
        }

        public async Task<Plant> CreatePlantAsync(Plant plant)
        {
            return await _repository.CreatePlantAsync(plant);
        }

        public async Task<Plant?> UpdatePlantAsync(Plant plant)
        {
            return await _repository.UpdatePlantAsync(plant) ?? throw new Exception("Plant not found");
        }

        public async Task DeletePlantAsync(int id)
        {
            await _repository.DeletePlantAsync(id);
        }
    }
}