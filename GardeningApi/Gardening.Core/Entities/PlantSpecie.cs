using Gardening.Core.Enums;

namespace Gardening.Core.Entities
{
    public class PlantSpecie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlantType Type { get; set; }
    }
}
