using BlazorChat.Models;
using BlazorChat.Shared;
using BlazorChat.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System;
using BlazorChat.Client.Models;
using static MudBlazor.Colors;


namespace BlazorChat.Client.Pages
{
    public partial class Movies
    {

        private MoviePagedResponse movies , movieName;
        private NewMovies upcomingMovies;
        private NewMovies nowplayingMovies;

        public string searchValue { get; set; }

        



        [SupplyParameterFromQuery]
        public int Page { get; set; } = 1;
        [SupplyParameterFromQuery]
        public int UpcomingPage { get; set; } = 1;
        [SupplyParameterFromQuery]
        public int NowPlayingPage { get; set; } = 1;
        [SupplyParameterFromQuery]
        public int SearchPage { get; set; } = 1;


        protected override async Task OnParametersSetAsync()
        {
            if ((Page ) < 1) Page = 1;
            else if (Page > 500) Page = 500;

            if ((UpcomingPage) < 1) UpcomingPage = 1;
            else if (UpcomingPage > 500) UpcomingPage = 500;

            if ((NowPlayingPage) < 1) NowPlayingPage = 1;
            else if (NowPlayingPage > 500) NowPlayingPage = 500;

            if ((SearchPage) < 1) SearchPage = 1;
            else if (SearchPage > 500) SearchPage = 500;


            movies = await TMDB.GetPopularMoviesAsync(Page);


            upcomingMovies = await TMDB.GetUpcomingMoviesAsync(UpcomingPage);


            nowplayingMovies = await TMDB.GetNowPlayingMoviesAsync(NowPlayingPage);

            movieName = await TMDB.GetMovieNameAsync(searchValue, SearchPage);
        }


        private void GetPage(int pageNum)
        {
            string url = Nav.GetUriWithQueryParameter("page", pageNum);
            Nav.NavigateTo(url);
        }

        private void GetUpcomingPage(int pageNum)
        {
            string url = Nav.GetUriWithQueryParameter("upcomingpage", pageNum);
            Nav.NavigateTo(url);
        }

        private void GetNowPlayingPage(int pageNum)
        {
            string url = Nav.GetUriWithQueryParameter("nowplayingpage", pageNum);
            Nav.NavigateTo(url);
        }

        private void GetSearchPage(int pageNum)
        {
            string url = Nav.GetUriWithQueryParameter("searchpage", pageNum);
            Nav.NavigateTo(url);
        }




        private bool resetValueOnEmptyText;
        private bool coerceText;
        private bool coerceValue;
        private bool update;
        private string value2;
        private List<string> movieNames = new List<string>();
   


        private async Task<IEnumerable<string>> Search2(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            movieNames.Clear();
            update = false;
            searchValue = value;

            // if text is null or empty, don't return values (drop-down will not open)
            if (string.IsNullOrEmpty(value))
                return new string[0];
            movieName =  await TMDB.GetMovieNameAsync(value);
            update = true;
            foreach (var movie in movieName.Results) 
            {
                movieNames.Add("[ID: " + movie.Id + "] " + movie.Title);
            }
            return movieNames;
        }



    }
}
