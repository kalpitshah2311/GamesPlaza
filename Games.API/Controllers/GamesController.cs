using BlazorApp1.Models;
using Games.API.Data.GamesRepository;
using Microsoft.AspNetCore.Mvc;

namespace Games.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    { 
        private readonly ILogger<GamesController> _logger;
        private readonly IGamesRepository gamesRepository;
        public GamesController(ILogger<GamesController> logger,IGamesRepository gamesRepository)
        {
            _logger = logger;
            this.gamesRepository = gamesRepository;
        }

        [HttpGet("GetGames")]
        public async Task<IEnumerable<GameSummary>> GetGames(int pageNumber = 1, int pageSize = 10)
        {
            var games = await gamesRepository.GetGames(pageNumber, pageSize);
            return games;
        }

        [HttpGet("{id:Guid}")]
        public async Task<GameSummary> GetGameSummaryById(Guid Id)
        {
            var game = await gamesRepository.GetGameById(Id);
            ArgumentNullException.ThrowIfNull(game);
            return game;
        }

        [HttpGet("GetGenres")]
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await gamesRepository.GetGenres();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await gamesRepository.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            await gamesRepository.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] GameDetails game)
        {
            await gamesRepository.Add(game);
            return CreatedAtAction(nameof(GetGameSummaryById), new { id = game.Id }, game);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] GameDetails game)
        {
            var existingGame = await gamesRepository.GetGameById(game.Id);
            if (existingGame == null)
            {
                return NotFound();
            }

            await gamesRepository.Update(game);
            return NoContent();

        }

        private async Task<Genre> GetGenreById(Guid Id)
        {
            return await gamesRepository.GetGenreById(Id);
        } 
       
    }
}
