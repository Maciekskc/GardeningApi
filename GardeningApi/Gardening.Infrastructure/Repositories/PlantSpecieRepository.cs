using Gardening.Core.Entities;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories.Interfaces;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Repositories
{
    public class PlantSpecieRepository : IPlantSpecieRepository
    {
        private readonly PlantAppDbContext _context;

        public PlantSpecieRepository(PlantAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync()
        {
            return await _context.PlantSpecies.ToListAsync();
        }

        public async Task<Option<PlantSpecie>> GetPlantSpecieByIdAsync(int id)
        {
            return await _context.PlantSpecies.FindAsync(id);
        }

        public async Task<PlantSpecie> CreatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            _context.PlantSpecies.Add(plantSpecie);
            await _context.SaveChangesAsync();
            return plantSpecie;
        }

        public async Task<PlantSpecie?> UpdatePlantSpecieAsync(PlantSpecie plantSpecie)
        {
            _context.Entry(plantSpecie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return plantSpecie;
        }

        public async Task DeletePlantSpecieAsync(int id)
        {
            var plantSpecie = await _context.PlantSpecies.FindAsync(id);
            if (plantSpecie != null)
            {
                _context.PlantSpecies.Remove(plantSpecie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
