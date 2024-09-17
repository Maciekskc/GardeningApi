using Asp.Versioning;
using Gardening.Services.DTOs.Plant.Post;
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
        public async Task<ActionResult<PostPlantResponse>> Post(PostPlantRequest request)
        {
            var result = await plantService.CreatePlantAsync(request);
            return result.Match<ActionResult>(Ok, f => BadRequest(f.Message));
        }
    }
}
