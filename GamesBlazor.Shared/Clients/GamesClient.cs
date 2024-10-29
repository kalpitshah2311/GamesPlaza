
using BlazorApp1.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorApp1.Clients
{
    public class GamesClient
    {
        private static readonly string https = "https";
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly GenresClient genresClient;
        public GamesClient(IHttpClientFactory httpClientFactory,GenresClient genresClient)
        {

          _httpClientFactory = httpClientFactory;
            this.genresClient = genresClient;
        }


 


        public async Task<GameSummary[]> GetGames()
        {
            try
            {
                var client = _httpClientFactory.CreateClient(https);
                var result = await client.GetFromJsonAsync<GameSummary[]>("Games/GetGames");
                return result!.ToArray();
            }
            catch(Exception ex) {
            
                return null;
            }

        }

        public async Task UpdateGame(GameDetails game)
        {
            var client = _httpClientFactory.CreateClient(https);
            await client.PutAsJsonAsync("Games",game);

        }

        public async Task DeleteGame(string id)
        {
            var client = _httpClientFactory.CreateClient(https);
            await client.DeleteAsync($"Games/{id}");
        }
        public async Task AddGame(GameDetails game)
        {
            var client = _httpClientFactory.CreateClient(https);
            
            await client.PostAsJsonAsync("Games", game);
        }
        public async Task<GameDetails> GetGame(Guid Id)
        {
            var client = _httpClientFactory.CreateClient(https);
            var game = await client.GetFromJsonAsync<GameSummary>($"Games/{Id}");
            var genres = await genresClient.getGenres();
            var genre = genres.Single(genre => string.Equals(genre.Name, game.Genre, StringComparison.OrdinalIgnoreCase));
            return new GameDetails
            {
                Name = game.Name,
                Id = game.Id,
                GenreId = genre.Id,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
            };
        }

        //public async Task<string> Login(Login? login)
        //{
        //    var client =_httpClient.CreateClient("client");
        //    var response = await client.PostAsJsonAsync("UserAuth/auth",login);
        //    response.EnsureSuccessStatusCode();
        //    var obj = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<string>(obj);
        //    return result!;
        //}

        private async Task<GameSummary> GetGameSummaryById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient(https);
            var result = await client.GetFromJsonAsync<GameSummary>($"Games/{Id}");
            return result;
        }
        private async Task<Genre> GetGenreById(Guid Id)
        {
            var genres = await genresClient.getGenres();

            ArgumentNullException.ThrowIfNullOrWhiteSpace(Id.ToString());
            return genres.Single(genre => genre.Id == Id);
        }
    }
}
