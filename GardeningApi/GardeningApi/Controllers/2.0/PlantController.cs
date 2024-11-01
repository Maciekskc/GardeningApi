using Asp.Versioning;
using FluentValidation;
using Gardening.Services.DTOs.Plant.Post;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers._2._0
{
    [ApiController]
    [ApiVersion(2.0)]
    [Route("api/[controller]")]
    public class PlantController(IPlantService plantService, IValidator<PostPlantRequest> _postPlantRequestValidator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<PostPlantResponse>> Post(PostPlantRequest request)
        {
            var model = _postPlantRequestValidator.Validate(request);
            if (!model.IsValid)
            {
                return BadRequest(model);
            }

            var result = await plantService.CreatePlantAsync(request);
            return result.Match<ActionResult>(Ok, f => BadRequest(f.Message));
        }
    }
}
