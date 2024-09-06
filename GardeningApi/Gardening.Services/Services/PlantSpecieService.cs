using Gardening.Core.Entities;
using Gardening.Infrastructure.Repositories.Interfaces;
using Gardening.Services.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;

namespace Gardening.Services.Services
{
    public class PlantSpecieService : IPlantSpecieService
    {
        private readonly IPlantSpecieRepository _repository;

        public PlantSpecieService(IPlantSpecieRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync()
        {
            return await _repository.GetAllPlantSpeciesAsync();
        }

        public async Task<Result<PlantSpecie>> GetPlantSpecieByIdAsync(int id)
        {
            return await _repository.GetPlantSpecieByIdAsync(id);
        }

        public async Task<PlantSpecie> CreatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            return await _repository.CreatePlantSpecieAsync(plantSpecie);
        }

        public async Task<Option<PlantSpecie>> UpdatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            return await _repository.UpdatePlantSpecieAsync(plantSpecie);
        }

        public async Task DeletePlantSpecieAsync(int id)
        {
            await _repository.DeletePlantSpecieAsync(id);
        }
    }
}