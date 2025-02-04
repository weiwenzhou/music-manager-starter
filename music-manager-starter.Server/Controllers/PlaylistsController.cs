using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Server.Data;
using music_manager_starter.Shared.Models;

namespace music_manager_starter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public PlaylistsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET: api/Playlists
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists.Include(p => p.Songs).ToListAsync();
        }

        [HttpPost] // POST: api/Playlists
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            if (playlist == null)
            {
                return BadRequest("Playlist cannot be null.");
            }
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")] // GET: api/Playlists/5
        public async Task<ActionResult<Playlist>> GetPlaylist(Guid id)
        {
            var playlist = await _context.Playlists.Include(p => p.Songs).FirstOrDefaultAsync(p => p.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        [HttpPost("{id}/songs/{songId}")] // POST: api/Playlists/5/songs/5
        public async Task<ActionResult<Playlist>> AddSongToPlaylist(Guid id, Guid songId)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound("Playlist not found.");
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound("Song not found.");
            }

            if (!playlist.Songs.Any(s => s.Id == songId))
            {
                _context.Entry(song).State = EntityState.Unchanged;
                playlist.Songs.Add(song);

                _context.Entry(playlist).Collection(p => p.Songs).IsModified = true;
                await _context.SaveChangesAsync();
            }

            return playlist;
        }

        [HttpDelete("{id}/songs/{songId}")] // DELETE: api/Playlists/5/songs/5
        public async Task<ActionResult<Song>> DeleteSong(Guid id, Guid songId)
        {
            var playlist = await _context.Playlists.Include(p => p.Songs).FirstOrDefaultAsync(p => p.Id == id);
            if (playlist == null)
            {
                return NotFound("Playlist not found.");
            }

            var song = playlist.Songs.FirstOrDefault(s => s.Id == songId);

            if (song == null)
            {
                return NotFound("Song not found in this playlist.");
            }

            playlist.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
