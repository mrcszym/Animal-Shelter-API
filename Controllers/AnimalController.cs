using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly DataContext context;

        public AnimalController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimals()
        {
            return Ok(await this.context.Animals.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await this.context.Animals.FindAsync(id);
            if(animal == null)
            {
                return BadRequest("Animal not found.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult<List<Animal>>> AddAnimal(Animal animal)
        {
            this.context.Animals.Add(animal);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Animals.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Animal>>> UpdateAnimal(Animal request)
        {
            var animal = await this.context.Animals.FindAsync(request.Id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            animal.Name = request.Name;
            animal.PossibleBirthYear = request.PossibleBirthYear;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.Animals.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await this.context.Animals.FindAsync(id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            this.context.Animals.Remove(animal);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Animals.ToListAsync());
        }
    }
}
