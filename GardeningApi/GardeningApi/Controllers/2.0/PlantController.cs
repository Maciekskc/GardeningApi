using Asp.Versioning;
using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers._2._0
{
    [ApiController]
    [ApiVersion(2.0)]
    [Route("api/[controller]")]
    public class PlantController(IPlantService plantService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<PostPlantResponseDto>> Post(PostPlantRequestDto plant)
        {
            var result = await plantService.CreatePlantAsync(plant);
            return result.Match<ActionResult>(s => Ok(s), f => BadRequest(f.Message));
        }
    }
}
