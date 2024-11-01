using FluentValidation;

namespace Gardening.Services.DTOs.Plant.Post;

public class PostPlantRequestValidator : AbstractValidator<PostPlantRequest>
{
    public PostPlantRequestValidator()
    {
        RuleFor(plant => plant.Name).NotEmpty().NotNull().WithMessage("Plant name is required!");
    }
}