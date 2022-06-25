using BilgeAdamBlog.Common.Clients.Models;
using BilgeAdamBlog.Common.DTOs.Base;
using BilgeAdamBlog.Common.DTOs.Comment;
using BilgeAdamBlog.Common.DTOs.Post;
using System;
using System.Collections.Generic;

namespace BilgeAdamBlog.Common.DTOs.User
{
    public class UserResponse : BaseDto
    {
        public UserResponse()
        {
            Posts = new HashSet<PostResponse>();
            Comments = new HashSet<CommentResponse>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<PostResponse> Posts { get; set; }
        public virtual ICollection<CommentResponse> Comments { get; set; }

        public GetAccessToken AccessToken { get; set; }
    }
}
