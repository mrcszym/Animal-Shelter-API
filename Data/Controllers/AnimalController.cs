using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using AnimalShelter.Manager;
using AnimalShelter.Data.Models;

namespace AnimalShelter.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly DataContext context;
        private readonly JwtAuthManager jwtAuthManager;

        public AnimalController(DataContext context, JwtAuthManager jwtAuthManager)
        {
            this.context = context;
            this.jwtAuthManager = jwtAuthManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<Animal>>> AddAnimal(Animal animal)
        {
            context.Animals.Add(animal);
            await context.SaveChangesAsync();
            return Ok(await context.Animals.ToListAsync());
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<List<Animal>>> UpdateAnimal(Animal request)
        {
            var animal = await context.Animals.FindAsync(request.Id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            animal.Name = request.Name;
            animal.PossibleBirthYear = request.PossibleBirthYear;

            await context.SaveChangesAsync();

            return Ok(await context.Animals.ToListAsync());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await context.Animals.FindAsync(id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            context.Animals.Remove(animal);
            await context.SaveChangesAsync();

            return Ok(await context.Animals.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimals()
        {
            return Ok(await context.Animals.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await context.Animals.FindAsync(id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            return Ok(animal);
        }
    }

    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
