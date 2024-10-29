using BlazorApp1.Models;
using Games.API.Data.GamesRepository;
using Microsoft.EntityFrameworkCore;

namespace Games.API.Data.GamesRepository
{
    public interface IGamesRepository
    {
        Task Add(GameDetails game);
        Task<GameSummary> GetGameById(Guid Id);
        Task Update(GameDetails game);
        Task Delete(Guid Id);
        Task<Genre> GetGenreById(Guid Id);
        Task<Genre[]> GetGenres();
        Task<GameSummary[]> GetGames(int pageNumber, int pageSize);
    }

public class GamesRepository : IGamesRepository
{
    private readonly CosmosDbContext _context;

        public GamesRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task Add(GameDetails game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameSummary> GetGameById(Guid id)
        {
            var gameDetails = await _context.Games.FindAsync(id);
            if (gameDetails != null)
            {
                var genre = await _context.Genres.FindAsync(gameDetails.GenreId);
                return new GameSummary
                {
                    Id = gameDetails.Id,
                    Name = gameDetails.Name,
                    Genre = genre?.Name,
                    Price = gameDetails.Price,
                    ReleaseDate = gameDetails.ReleaseDate,
                };
            }

            return null;
        }

        public async Task<GameSummary[]> GetGames(int pageNumber, int pageSize)
        {
            var games = await _context.Games
                .OrderBy(game => game.Name) // You can change this to any property you'd like to sort by
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            var gameSummaries = games.Select(game => new GameSummary
            {
                Id = game.Id,
                Name = game.Name,
                Genre = _context.Genres.FirstOrDefault(g => g.Id == game.GenreId)?.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            }).ToArray();

            return gameSummaries;
        }

        public async Task<Genre> GetGenreById(Guid id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre[]> GetGenres()
        {
            return await _context.Genres.ToArrayAsync();
        }

        public async Task Update(GameDetails game)
        {
            var existingGame = await _context.Games.FindAsync(game.Id);
            if (existingGame != null)
            {
                existingGame.Name = game.Name;
                existingGame.GenreId = game.GenreId;
                existingGame.Price = game.Price;
                existingGame.ReleaseDate = game.ReleaseDate;

                _context.Games.Update(existingGame);
                await _context.SaveChangesAsync();
            }
        }
    }
}
