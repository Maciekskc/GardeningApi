using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [HttpGet]
        public async Task<IEnumerable<Plant>> Get()
        {
            return await _plantService.GetAllPlantsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> Get(int id)
        {
            var plant = await _plantService.GetPlantByIdAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return plant;
        }

        [HttpPost]
        public async Task<ActionResult<Plant>> Post(Plant plant)
        {
            var createdPlant = await _plantService.CreatePlantAsync(plant);
            return CreatedAtAction(nameof(Get), new { id = createdPlant.Id }, createdPlant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Plant plant)
        {
            if (id != plant.Id)
            {
                return BadRequest();
            }

            await _plantService.UpdatePlantAsync(plant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _plantService.DeletePlantAsync(id);
            return NoContent();
        }
    }
}