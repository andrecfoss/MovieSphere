using BlazorChat.Models;
using BlazorChat.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorChat.Client.Managers
{
    public class MoviesManager : IMoviesManager
    {

        private readonly HttpClient _httpClient;

        public MoviesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task <MoviePagedResponse> GetMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MoviePagedResponse>("api/chat/movies");
            return data;
        }
    }
}
