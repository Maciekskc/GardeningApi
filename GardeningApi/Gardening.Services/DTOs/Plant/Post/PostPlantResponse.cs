namespace Gardening.Services.DTOs.Plant.Post
{
    public record PostPlantResponse
    {
        public string? Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public string? Specie { get; set; }
    }
}
