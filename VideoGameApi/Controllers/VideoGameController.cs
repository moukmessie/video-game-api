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

		

		//[HttpGet]
		//public ActionResult<IEnumerable<VideoGame>> GetVideoGames()
		//{
		//	return Ok(_videoGames);
		//}

		//[HttpGet("{id}")]
		//public ActionResult<VideoGame> GetVideoGameById(int id)
		//{
		//	var videoGame = _videoGames.FirstOrDefault(v => v.Id == id);
		//	if (videoGame == null)
		//		return NotFound();

		//	return Ok(videoGame);
		//}

		//[HttpPost]
		//public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
		//{
		//	if (newGame is null)
		//		return BadRequest();

		//	newGame.Id = _videoGames.Max(g => g.Id) + 1;
		//	_videoGames.Add(newGame);

		//	return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
		//}

		//[HttpPut("{id}")]
		//public IActionResult UpdateVideoGame(int id, VideoGame updateGame)
		//{
		//	var game = _videoGames.FirstOrDefault(g => g.Id == id);
		//	if (game is null)
		//		return NotFound();

		//	game.Title = updateGame.Title;
		//	game.Publisher = updateGame.Publisher;
		//	game.Developer = updateGame.Developer;
		//	game.Platform = updateGame.Platform;

		//	return NoContent();
		//}

		//[HttpDelete("{id}")]
		//public IActionResult DeleteVideoGame (int id)
		//{
		//	var game = _videoGames.FirstOrDefault(g => g.Id == id);
		//	if (game is null)
		//		return NotFound();

		//	_videoGames.Remove(game);
		//	return NoContent();

		//}

	}
}
