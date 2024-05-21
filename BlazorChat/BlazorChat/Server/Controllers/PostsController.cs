using BlazorChat.Server.Data;
using BlazorChat.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public PostsController( ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPostsAsync()
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.PostMessages
                    .Where(h => (h.FromUserId != userId))
                    .OrderByDescending(a => a.CreatedDate)
                    .Select(x => new PostMessage
                    {
                        FromUserId = x.FromUserId,
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        UserName = x.UserName,
                        Image = x.Image,
                    }).ToListAsync();
            return Ok(messages);
        }


        [HttpGet("own")]
        public async Task<IActionResult> GetOwnPostsAsync()
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.PostMessages
                    .Where(h => (h.FromUserId == userId))
                    .OrderByDescending(a => a.CreatedDate)
                    .Select(x => new PostMessage
                    {
                        FromUserId = x.FromUserId,
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        UserName = x.UserName,
                        Image = x.Image,
                    }).ToListAsync();
            return Ok(messages);
        }





        [HttpPost]
        public async Task<IActionResult> SavePostAsync(PostMessage post)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            post.FromUserId = userId;
            post.CreatedDate = DateTime.Now;
            await _context.PostMessages.AddAsync(post);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPost("comment")]
        public async Task<IActionResult> SaveCommentPostAsync(CommentsMessage comment)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            comment.FromUserId = userId;
            comment.CreatedDate = DateTime.Now;
            await _context.CommentsMessages.AddAsync(comment);
            return Ok(await _context.SaveChangesAsync());
        }


        [HttpGet("comments/all")]
        public async Task<IActionResult> GetCommentAsync()
        {
      
            var messages = await _context.CommentsMessages
                    .OrderByDescending(a => a.CreatedDate)
                    .Select(x => new CommentsMessage
                    {
                        FromUserId = x.FromUserId,
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        UserName = x.UserName,
                        PostMessageId = x.PostMessageId,
                    }).ToListAsync();
            return Ok(messages);
        }





    }
}

