using BlazorChat.Models;
using BlazorChat.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.Client.Managers
{
    public interface IMoviesManager
    {
        Task<MoviePagedResponse> GetMoviesAsync();
    }
}
