
using BlazorApp1.Models;
using System.Net.Http.Json;

namespace BlazorApp1.Clients
{
    public class GenresClient
    {
        private static readonly string https = "https";
        private readonly IHttpClientFactory _httpClientFactory;
        public GenresClient(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
        }
        public async Task<Genre[]> getGenres()
        {
            try
            {
                var client = _httpClientFactory.CreateClient(https);
                var result = await client.GetFromJsonAsync<Genre[]>("Games/GetGenres");
                return result!.ToArray();
            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
