using System.Security.Cryptography.X509Certificates;
using Gardening.Core.Entities;
using Gardening.Infrastructure.Repositories.Interfaces;
using Gardening.Services.Services.Interfaces;
using LanguageExt.Common;
using LanguageExt.SomeHelp;

namespace Gardening.Services.Services
{
    public class PlantSpecieService(IPlantSpecieRepository repository) : IPlantSpecieService
    {
        public async Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync()
        {
            return await repository.GetAllPlantSpeciesAsync();
        }

        public async Task<Result<PlantSpecie>> GetPlantSpecieByIdAsync(int id)
        {
            var result = await repository.GetPlantSpecieByIdAsync(id);
            return result.Match(
                obj => new Result<PlantSpecie>(obj),
                () => new Result<PlantSpecie>(new Exception("The object does not exist")));
        }


        public async Task<Result<PlantSpecie>> CreatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            var result = await repository.CreatePlantSpecieAsync(plantSpecie);
            return result.Match(obj => new Result<PlantSpecie>(obj),
                () => new Result<PlantSpecie>(new Exception("Object was not created")));
        }

        public async Task<Result<PlantSpecie>> UpdatePlantSpecieAsync(int id, PlantSpecie plantSpecie)
        {
            if (id == default || id < 0)
            {
                return new Result<PlantSpecie>(new Exception("Invalid id"));
            }

            if (id != plantSpecie.Id)
            {
                return new Result<PlantSpecie>(new Exception("You cannot change record Id"));
            }

            var existingPlant = await repository.GetPlantSpecieByIdAsync(id);
            if (existingPlant.IsNone)
            {
                return new Result<PlantSpecie>(new Exception("Plant Specie not found in the database, nothing to update"));
            }

            var result = await repository.UpdatePlantSpecieAsync(plantSpecie);
            return result.Match(obj => new Result<PlantSpecie>(obj),
                () => new Result<PlantSpecie>(new Exception("Object was not updated")));
        }

        public async Task<Result<int>> DeletePlantSpecieAsync(int id)
        {
            var repositorySearchResult = await repository.GetPlantSpecieByIdAsync(id);
            return await repositorySearchResult.Match<Task<Result<int>>>(
                async plantSpecie =>
                {
                    var deleteResult = await repository.DeletePlantSpecieAsync(plantSpecie);
                    return deleteResult.Match(
                        obj => new Result<int>(obj),
                        () => new Result<int>(new Exception("Object was not removed")));
                }, () => Task.FromResult(new Result<int>(new Exception("Plant species not found")))
            );
        }
    }
}