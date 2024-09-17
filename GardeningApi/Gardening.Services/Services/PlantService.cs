using Gardening.Core.Entities;
using Gardening.Core.Interfaces;
using Gardening.Services.DTOs.Plant.Post;
using Gardening.Services.Mappings;
using Gardening.Services.Services.Interfaces;
using LanguageExt.Common;

namespace Gardening.Services.Services
{
    public class PlantService(IPlantRepository repository, IPlantSpecieService plantSpecieService) : IPlantService
    {
        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            return await repository.GetAllPlantsAsync();
        }

        public async Task<Result<Plant>> GetPlantByIdAsync(int id)
        {
            var result = await repository.GetPlantByIdAsync(id);
            return result.Match(
                obj => new Result<Plant>(obj),
                () => new Result<Plant>(new Exception("The object does not exist")));
        }

        public async Task<Result<Plant>> CreatePlantAsync(Plant plant)
        {
            var result = await repository.CreatePlantAsync(plant);
            return result.Match(obj => new Result<Plant>(obj),
                () => new Result<Plant>(new Exception("Object was not created")));
        }

        public async Task<Result<PostPlantResponse>> CreatePlantAsync(PostPlantRequest request)
        {
            var plantSpecie = await plantSpecieService.GetPlantSpecieByNameAsync(request.Specie);

            if (plantSpecie.IsFaulted)
            {
                return new Result<PostPlantResponse>(new Exception("Not existing Plant Specie"));
            }

            var plant = PlantMapper.PostPlantRequestToPlant(request);

            if (plant.PlantingDate == default)
            {
                plant.PlantingDate = DateTime.UtcNow;
            }

            plantSpecie.IfSucc(result => plant.PlantSpecie = result);

            var result = await repository.CreatePlantAsync(plant);
            return result.Match(obj => new Result<PostPlantResponse>(PlantMapper.PlantToPostPlantResponse(obj)),
                () => new Result<PostPlantResponse>(new Exception("Object was not created")));
        }

        public async Task<Result<Plant>> UpdatePlantAsync(int id, Plant plant)
        {
            if (id == default || id < 0)
            {
                return new Result<Plant>(new Exception("Invalid id"));
            }

            if (id != plant.Id)
            {
                return new Result<Plant>(new Exception("You cannot change record Id"));
            }

            var existingPlant = await repository.GetPlantByIdAsync(id);
            if (existingPlant.IsNone)
            {
                return new Result<Plant>(new Exception("Plant not found in the database, nothing to update"));
            }

            var result = await repository.UpdatePlantAsync(plant);
            return result.Match(obj => new Result<Plant>(obj),
                () => new Result<Plant>(new Exception("Object was not updated")));
        }

        public async Task<Result<int>> DeletePlantAsync(int id)
        {
            var repositorySearchResult = await repository.GetPlantByIdAsync(id);
            return await repositorySearchResult.Match<Task<Result<int>>>(
                async plant =>
                {
                    var deleteResult = await repository.DeletePlantAsync(plant);
                    return deleteResult.Match(
                        obj => new Result<int>(obj),
                        () => new Result<int>(new Exception("Object was not removed")));
                }, () => Task.FromResult(new Result<int>(new Exception("Plant not found")))
            );
        }
    }
}