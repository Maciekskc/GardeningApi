using Asp.Versioning;
using Gardening.Core.Entities;
using Gardening.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GardeningApi.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/[controller]")]
    public class PlantSpecieController(IPlantSpecieService plantSpecieService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<PlantSpecie>> Get()
        {
            return await plantSpecieService.GetAllPlantSpeciesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlantSpecie>> Get(int id)
        {
            var result = await plantSpecieService.GetPlantSpecieByIdAsync(id);
            return result.Match<ActionResult>(s => Ok(s), f => NotFound(f.Message));
        }

        [HttpPost]
        public async Task<ActionResult<PlantSpecie>> Post(PlantSpecie plantSpecie)
        {
            var result = await plantSpecieService.CreatePlantSpecieAsync(plantSpecie);
            return result.Match<ActionResult>(s => Ok(s), f => BadRequest(f.Message));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PlantSpecie plantSpecie)
        {
            var result = await plantSpecieService.UpdatePlantSpecieAsync(id, plantSpecie);
            return result.Match<IActionResult>(s=> Ok(s), f => BadRequest(f.Message));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await plantSpecieService.DeletePlantSpecieAsync(id);
            return result.Match<IActionResult>(s => NoContent(), f => BadRequest(f.Message));
        }
    }
}