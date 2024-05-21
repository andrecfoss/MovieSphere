using BlazorChat.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.Client.Managers
{
    public interface IPostManager
    {
        Task SaveMessageAsync(PostMessage postMessage);

        Task SaveCommentMessageAsync(CommentsMessage commentMessage);

        Task<List<CommentsMessage>> GetCommentAsync();


        Task<List<PostMessage>> GetPostsAsync();

        Task<List<PostMessage>> GetOwnPostsAsync();
    }
}
