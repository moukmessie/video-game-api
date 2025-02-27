using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController(VideoGameDbContext context) : ControllerBase
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
			if (videoGame is null)
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
		public async  Task <IActionResult> UpdateVideoGame(int id, VideoGame updateGame)
		{
			var game = await _context.VideoGames.FindAsync(id);
			if (game is null)
				return NotFound();

			game.Title = updateGame.Title;
			game.Publisher = updateGame.Publisher;
			game.Developer = updateGame.Developer;
			game.Platform = updateGame.Platform;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteVideoGame(int id)
		{
			var videoGame = await _context.VideoGames.FindAsync(id);
			if (videoGame is null)
				return NotFound();
			_context.VideoGames.Remove(videoGame);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		

	}
}
