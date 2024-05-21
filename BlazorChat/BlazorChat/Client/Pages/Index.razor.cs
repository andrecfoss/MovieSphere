using BlazorChat.Client.Models;
using BlazorChat.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.Client.Pages
{
    public partial class Index
    {


        private MoviePagedResponse topRatedMovies;
        private NewMovies upcomingMovies;
        private NewMovies nowplayingMovies;

        [SupplyParameterFromQuery]
        public int Page { get; set; } = 1;



        protected override async Task OnParametersSetAsync()
        {
            if (Page < 1) Page = 1;
            else if (Page > 500) Page = 500;

            topRatedMovies = await TMDB.GetTopRatedMoviesAsync(Page);

            upcomingMovies = await TMDB.GetUpcomingMoviesAsync(Page);

            nowplayingMovies = await TMDB.GetNowPlayingMoviesAsync(Page);
        }


    }
}
