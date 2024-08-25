namespace Gardening.Core.Entities
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public int PlantSpecieId { get; set; }
        public PlantSpecie PlantSpecie { get; set; }
    }
}
