using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using AnimalShelter.Data.Models;

namespace AnimalShelter.Data.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : Controller
    {
        private readonly DataContext _context;
        public AnimalController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost(Name = "AddAnimal")]
        public async Task<ActionResult<List<Animal>>> AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return Ok(await _context.Animals.ToListAsync());
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<List<Animal>>> UpdateAnimal(Animal request)
        {
            var animal = await _context.Animals.FindAsync(request.Id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            animal.Name = request.Name;
            animal.PossibleBirthYear = request.PossibleBirthYear;

            await _context.SaveChangesAsync();

            return Ok(await _context.Animals.ToListAsync());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return BadRequest("Animal not found.");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return Ok(await _context.Animals.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimals()
        {
            return Ok(await _context.Animals.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
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
