using Gardening.Core.Entities;
using Gardening.Core.Interfaces;
using Gardening.Infrastructure.Data;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Repositories
{
    public class PlantSpecieRepository(PlantAppDbContext context) : IPlantSpecieRepository
    {
        public async Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync()
        {
            return await context.PlantSpecies.ToListAsync();
        }

        public async Task<Option<PlantSpecie>> GetPlantSpecieByIdAsync(int id)
        {
            return await context.PlantSpecies.FindAsync(id);
        }

        public async Task<Option<PlantSpecie>> CreatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            context.PlantSpecies.Add(plantSpecie);
            await context.SaveChangesAsync();
            return plantSpecie;
        }

        public async Task<Option<PlantSpecie>> UpdatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            context.PlantSpecies.Update(plantSpecie);
            await context.SaveChangesAsync();
            return plantSpecie;
        }

        public async Task<Option<int>> DeletePlantSpecieAsync(PlantSpecie plantSpacie)
        {
            context.PlantSpecies.Remove(plantSpacie);
            return await context.SaveChangesAsync();
        }
    }
}
