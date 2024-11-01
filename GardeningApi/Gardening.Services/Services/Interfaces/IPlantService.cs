using Gardening.Core.Entities;
using Gardening.Services.DTOs.Plant.Post;
using LanguageExt.Common;

namespace Gardening.Services.Services.Interfaces
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
        Task<Result<Plant>> GetPlantByIdAsync(int id);
        Task<Result<Plant>> CreatePlantAsync(Plant plant);
        Task<Result<PostPlantResponse>> CreatePlantAsync(PostPlantRequest request);
        Task<Result<Plant>> UpdatePlantAsync(int id, Plant plant);
        Task<Result<int>> DeletePlantAsync(int id);
    }
}