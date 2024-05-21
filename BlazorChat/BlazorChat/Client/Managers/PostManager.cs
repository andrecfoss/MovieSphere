using BlazorChat.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorChat.Client.Managers
{
    public class PostManager : IPostManager
    {

        private readonly HttpClient _httpClient;

        public PostManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PostMessage>> GetOwnPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostMessage>>($"api/posts/own");
        }

        public async Task<List<PostMessage>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostMessage>>($"api/posts/all");
        }


        public async Task SaveMessageAsync(PostMessage postMessage)
        {
            await _httpClient.PostAsJsonAsync("api/posts", postMessage);
        }

        public async Task SaveCommentMessageAsync(CommentsMessage commentMessage)
        {
            await _httpClient.PostAsJsonAsync("api/posts/comment", commentMessage);
        }

        public async Task<List<CommentsMessage>> GetCommentAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CommentsMessage>>($"api/posts/comments/all");
        }
    }
}
