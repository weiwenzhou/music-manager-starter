using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Server.Data;
using music_manager_starter.Shared.Models;
using System;

namespace music_manager_starter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public SongsController(DataDbContext context)
        {
            _context = context;
        }

  
        [HttpGet] // GET: api/Songs
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        [HttpPost] // POST: api/Songs
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            if (song == null)
            {
                return BadRequest("Song cannot be null.");
            }


            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")] // GET: api/Songs/5
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return song;
        }

        [HttpDelete("{id}")] // DELETE: api/Songs/5
        public async Task<ActionResult<Song>> DeleteSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            
            return NoContent(); // 204 No Content (successful deletion)
        }
    }
}
