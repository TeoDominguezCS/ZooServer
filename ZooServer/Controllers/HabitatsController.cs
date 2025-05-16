using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zooModel;
using ZooServer.DTOS;

namespace ZooServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitatsController : ControllerBase
    {
        private readonly ProjectSourceContext _context;

        public HabitatsController(ProjectSourceContext context)
        {
            _context = context;
        }

        // GET: api/Habitats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitat>>> GetHabitats()
        {
            return await _context.Habitats.ToListAsync();
        }

        [Authorize]
        // GET: api/Habitats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitat>> GetHabitat(int id)
        {
            var habitat = await _context.Habitats.FindAsync(id);

            if (habitat == null)
            {
                return NotFound();
            }

            return habitat;
        }

        [Authorize]
        [HttpGet("GetPopulation/{id}")]
        public async Task<ActionResult<HabitatPopulation>> GetHabitatPopulation(int id)
        {
            HabitatPopulation habitat = await _context.Habitats.Where(habitat => habitat.HabitatId == id).Select(habitat => new HabitatPopulation
            {
                HabitatId = habitat.HabitatId,
                Name = habitat.Name,
                TotalPopulaton = habitat.Animals.Sum(c => c.Population),
            }).SingleAsync();
            return habitat;
        }


        // PUT: api/Habitats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitat(int id, Habitat habitat)
        {
            if (id != habitat.HabitatId)
            {
                return BadRequest();
            }

            _context.Entry(habitat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Habitats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Habitat>> PostHabitat(Habitat habitat)
        {
            _context.Habitats.Add(habitat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHabitat", new { id = habitat.HabitatId }, habitat);
        }

        // DELETE: api/Habitats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            var habitat = await _context.Habitats.FindAsync(id);
            if (habitat == null)
            {
                return NotFound();
            }

            _context.Habitats.Remove(habitat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HabitatExists(int id)
        {
            return _context.Habitats.Any(e => e.HabitatId == id);
        }

    }
}
