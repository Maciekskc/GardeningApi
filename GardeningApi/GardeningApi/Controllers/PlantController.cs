using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantController(IPlantService plantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Plant>> Get()
        {
            return await plantService.GetAllPlantsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> Get(int id)
        {
            var result = await plantService.GetPlantByIdAsync(id);
            return result.Match<ActionResult>(s => Ok(s), f => NotFound(f.Message));
        }

        [HttpPost]
        public async Task<ActionResult<Plant>> Post(Plant plant)
        {
            var result = await plantService.CreatePlantAsync(plant);
            return result.Match<ActionResult>(s => Ok(s), f => BadRequest(f.Message));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Plant plant)
        {
            var result = await plantService.UpdatePlantAsync(id, plant);
            return result.Match<IActionResult>(s => Ok(s), f => BadRequest(f.Message));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await plantService.DeletePlantAsync(id);
            return result.Match<IActionResult>(s => NoContent(), f => BadRequest(f.Message));
        }
    }
}