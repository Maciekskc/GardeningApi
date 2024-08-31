using Gardening.Core.Entities;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly PlantAppDbContext _context;

        public PlantRepository(PlantAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            return await _context.Plants.ToListAsync();
        }

        public async Task<Plant?> GetPlantByIdAsync(int id)
        {
            return await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Plant> CreatePlantAsync(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
            return plant;
        }

        public async Task<Plant?> UpdatePlantAsync(Plant plant)
        {
            _context.Entry(plant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return plant;
        }

        public async Task DeletePlantAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant != null)
            {
                _context.Plants.Remove(plant);
                await _context.SaveChangesAsync();
            }
        }
    }
}