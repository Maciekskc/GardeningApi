using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantSpecieController : ControllerBase
    {
        private readonly IPlantSpecieService _plantSpecieService;

        public PlantSpecieController(IPlantSpecieService plantSpecieService)
        {
            _plantSpecieService = plantSpecieService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlantSpecie>> Get()
        {
            return await _plantSpecieService.GetAllPlantSpeciesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlantSpecie>> Get(int id)
        {
            var plantSpecie = await _plantSpecieService.GetPlantSpecieByIdAsync(id);
            if (plantSpecie == null)
            {
                return NotFound();
            }
            return plantSpecie;
        }

        [HttpPost]
        public async Task<ActionResult<PlantSpecie>> Post(PlantSpecie plantSpecie)
        {
            var createdPlantSpecie = await _plantSpecieService.CreatePlantSpecieAsync(plantSpecie);
            return CreatedAtAction(nameof(Get), new { id = createdPlantSpecie.Id }, createdPlantSpecie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PlantSpecie plantSpecie)
        {
            if (id != plantSpecie.Id)
            {
                return BadRequest();
            }

            await _plantSpecieService.UpdatePlantSpecieAsync(plantSpecie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _plantSpecieService.DeletePlantSpecieAsync(id);
            return NoContent();
        }
    }
}