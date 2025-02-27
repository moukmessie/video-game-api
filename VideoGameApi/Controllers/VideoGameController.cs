using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {
       private readonly VideoGameDbContext _context = context ;


		[HttpGet]
		public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
		{
			return Ok(await _context.VideoGames.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
		{
			var videoGame = await _context.VideoGames.FindAsync(id);
			if (videoGame == null)
				return NotFound();
			return Ok(videoGame);
		}

		[HttpPost]
		public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
		{
			if (newGame is null)
				return BadRequest();

			_context.VideoGames.Add(newGame);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateVideoGame(int id, VideoGame updateGame)
		{
			if (id != updateGame.Id)
				return BadRequest();
			_context.Entry(updateGame).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!VideoGameExists(id))
					return NotFound();
				else
					throw;
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteVideoGame(int id)
		{
			var videoGame = await _context.VideoGames.FindAsync(id);
			if (videoGame == null)
				return NotFound();
			_context.VideoGames.Remove(videoGame);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		private bool VideoGameExists(int id)
		{
			return _context.VideoGames.Any(e => e.Id == id);
		}

	}
}
