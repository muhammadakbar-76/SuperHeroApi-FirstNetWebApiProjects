using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Models;


namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //private static List<SuperHero> heroes = new()
        //     {
        //        new SuperHero {
        //            Id = 1,
        //            Name ="Mario",
        //            FirstName="Anjay",
        //            LastName="Anjayni",
        //            MyProperty="New York"
        //        },
        //        new SuperHero
        //        {
        //            Id = 2,
        //            Name = "Luigi",
        //            FirstName = "Lui",
        //            LastName = "Gi",
        //            MyProperty = "New York"
        //        },
        //};

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        { 
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetOneHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            //from her in heroes
            // where her.Id == id
            // select her; //this one return an array of SuperHero type
            //you could also use heroes[id]
            if (hero == null) return NotFound("Hero Not Found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero suphero)
        {
            _context.SuperHeroes.Add(suphero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero suphero)
        {
            var hero = await _context.SuperHeroes.FindAsync(suphero.Id);
            if (hero == null) return NotFound("Hero not found");
            hero.Name = suphero.Name;
            hero.FirstName = suphero.FirstName;
            hero.LastName = suphero.LastName;
            hero.MyProperty = suphero.MyProperty;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return NotFound("Hero Not found");
            
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
