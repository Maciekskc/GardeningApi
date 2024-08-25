using Gardening.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gardening.Infrastructure.Data
{
    public class PlantAppDbContext : DbContext
    {
        public PlantAppDbContext(DbContextOptions<PlantAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantSpecie> PlantSpecies { get; set; }
    }
}