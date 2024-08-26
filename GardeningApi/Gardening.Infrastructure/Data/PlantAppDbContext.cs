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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plant>()
                .HasOne(p => p.PlantSpecie)
                .WithMany() 
                .HasForeignKey(p => p.PlantSpecieId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}