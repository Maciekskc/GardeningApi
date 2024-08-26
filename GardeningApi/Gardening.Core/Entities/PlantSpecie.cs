using Gardening.Core.Enums;

namespace Gardening.Core.Entities
{
    public record PlantSpecie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlantType Type { get; set; }
    }
}
