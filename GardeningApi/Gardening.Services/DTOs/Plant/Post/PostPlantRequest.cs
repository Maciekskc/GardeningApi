namespace Gardening.Services.DTOs.Plant.Post;

public record PostPlantRequest()
{
    public string? Name { get; set; }
    public string? Specie { get; set; }
}