using System.Runtime.CompilerServices;

namespace Gardening.Core.Entities
{
    public record Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Plant";
        public DateTime PlantingDate { get; set; }
        public int? PlantSpecieId { get; set; }
        public PlantSpecie? PlantSpecie { get; set; }
    }
}
