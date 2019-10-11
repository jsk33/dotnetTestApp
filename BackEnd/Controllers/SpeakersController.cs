using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    // the route attribute: what location/address on the web server is listening to and this controller is gonna respond to?
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // we are passing in a required dependency into the constructor so that other methods can use it
        // this is called: Dependency Injection
        public SpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Speakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speaker>>> GetSpeakers()
        {
            return await _context.Speakers.ToListAsync();

            // IEnumerable says: it's a collection that can be read through by going forward only
            // ActionResult says: format this into something the web can read
            // Task says: do this asynchronously/run in background, but make sure you come back and return this
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Speaker>> GetSpeaker(int id)
        {
            var speaker = await _context.Speakers.FindAsync(id);

            if (speaker == null)
            {
                return NotFound();
                // 404: file not found status code
            }

            return speaker;
        }

        // PUT: api/Speakers/5
        // put does an update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker(int id, Speaker speaker)
        {
            if (id != speaker.ID)
            {
                return BadRequest();
            }

            _context.Entry(speaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // POST: api/Speakers
        [HttpPost]
        public async Task<ActionResult<Speaker>> PostSpeaker(Speaker speaker)
        {
            _context.Speakers.Add(speaker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeaker", new { id = speaker.ID }, speaker);
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Speaker>> DeleteSpeaker(int id)
        {
            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }

            _context.Speakers.Remove(speaker);
            await _context.SaveChangesAsync();

            return speaker;
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speakers.Any(e => e.ID == id);
        }
    }
}
