using Gardening.Core.Entities;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories.Interfaces;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Repositories
{
    public class PlantRepository(PlantAppDbContext context) : IPlantRepository
    {
        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            return await context.Plants.ToListAsync();
        }

        public async Task<Option<Plant>> GetPlantByIdAsync(int id)
        {
            return await context.Plants.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Option<Plant>> CreatePlantAsync(Plant plant)
        {
            context.Plants.Add(plant);
            await context.SaveChangesAsync();
            return plant;
        }

        public async Task<Option<Plant>> UpdatePlantAsync(Plant plant)
        {
            context.Entry(plant).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return plant;
        }

        public async Task<Option<int>> DeletePlantAsync(Plant plant)
        {
            context.Plants.Remove(plant);
            return await context.SaveChangesAsync();
        }
    }
}