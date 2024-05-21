using BlazorChat.Client.Models;
using BlazorChat.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorChat.Services
{
    public class TMDBClient
    {
        private readonly HttpClient _httpClient;

        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found!");
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public Task<MoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<MoviePagedResponse>($"movie/popular?page={page}");
        }

        public Task<MoviePagedResponse?> GetTopRatedMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<MoviePagedResponse>($"movie/top_rated?page={page}");
        }

        public Task<MoviePagedResponse?> GetMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<MoviePagedResponse>($"discover/movie?page={page}");
        }

        public Task<MoviePagedResponse?> GetMovieNameAsync(string query, int page =1)
        {
            return _httpClient.GetFromJsonAsync<MoviePagedResponse>($"search/movie?query={query}&page={page}");
        }


        public Task<NewMovies> GetUpcomingMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<NewMovies>($"movie/upcoming?page={page}");
        }


        public Task<NewMovies?> GetNowPlayingMoviesAsync(int page = 1)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<NewMovies>($"movie/now_playing?page={page}");
        }



        public Task<MovieDetails?> GetMovieDetailsAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }
    }
}
