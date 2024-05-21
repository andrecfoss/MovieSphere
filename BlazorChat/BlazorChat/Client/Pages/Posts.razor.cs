using BlazorChat.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace BlazorChat.Client.Pages
{
    public partial class Posts
    {

        [Parameter] public string CurrentMessage { get; set; }
        [Parameter] public string CurrentUserId { get; set; }
        [Parameter] public string CurrentUsername { get; set; }

        [Parameter] public string CurrentCommentMessage { get; set; }


        [Parameter] public string uploadedImage { get; set; }




        private ICollection<PostMessage> postMessages = new List<PostMessage>();

        private ICollection<CommentsMessage> commentsMessages = new List<CommentsMessage>();

      


        protected override async Task OnInitializedAsync()
        {

            Task<AuthenticationState> _currentAuthenticationStateTask;

            _stateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;

            _currentAuthenticationStateTask = _stateProvider.GetAuthenticationStateAsync();

            OnAuthenticationStateChanged(_currentAuthenticationStateTask);

            await LoadAllPosts();

            await LoadcommentPosts();





        }

        private void OnAuthenticationStateChanged
              (Task<AuthenticationState> authenticationStateTask)
        {
            _ = InvokeAsync(async () =>
            {
                var authState = await authenticationStateTask;
                var user = authState.User;

                CurrentUserId = user.Claims.Where(a => a.Type == "sub").Select(a => a.Value).FirstOrDefault();
                CurrentUsername = user.Claims.Where(a => a.Type == "name").Select(a => a.Value).FirstOrDefault();
                StateHasChanged();
            });
        }

        async Task LoadAllPosts()
        {
            postMessages = new List<PostMessage>();
            postMessages = await _postManager.GetPostsAsync();           
        }


        async Task LoadOwnPosts()
        {
            postMessages = new List<PostMessage>();
            postMessages = await _postManager.GetOwnPostsAsync();
        }


        async Task LoadcommentPosts()
        {
            commentsMessages = new List<CommentsMessage>();
            commentsMessages = await _postManager.GetCommentAsync();
        }

        IList<IBrowserFile> files = new List<IBrowserFile>();
        private  async void UploadFiles(IBrowserFile file)
        {
            var format = "image/png"; 
            files.Add(file);
            //TODO upload the files to the server

            var resizedImage = await file.RequestImageFileAsync(format, 600, 600);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            uploadedImage = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
            StateHasChanged();

        }




        private async Task SubmitPostAsync()
        {
            if (!string.IsNullOrEmpty(CurrentMessage))
            {
                //Save Message to DB
                var postMessage = new PostMessage()
                {
                    Message = CurrentMessage,
                    CreatedDate = DateTime.Now,
                    FromUserId = CurrentUserId,
                    UserName= CurrentUsername,
                    Image = uploadedImage,
                    



                };
                await _postManager.SaveMessageAsync(postMessage);
                postMessage.FromUserId = CurrentUserId;
                CurrentMessage = string.Empty;
                uploadedImage = string.Empty;
                StateHasChanged();  

            }
        }


        private async Task SubmitCommentAsync(long postId)
        {
            if (!string.IsNullOrEmpty(CurrentCommentMessage))
            {
                //Save Message to DB
                var commentMessage = new CommentsMessage()
                {
                    Message = CurrentCommentMessage,
                    CreatedDate = DateTime.Now,
                    FromUserId = CurrentUserId,
                    UserName = CurrentUsername,
                    PostMessageId = postId,

                };
                await _postManager.SaveCommentMessageAsync(commentMessage);
                commentMessage.FromUserId = CurrentUserId;
                CurrentCommentMessage = string.Empty;
                StateHasChanged() ;

            }
        }



    }
}

