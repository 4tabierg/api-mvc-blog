using BilgeAdamBlog.Common.DTOs.Base;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.Common.DTOs.Comment;
using BilgeAdamBlog.Common.DTOs.User;
using System;
using System.Collections.Generic;

namespace BilgeAdamBlog.Common.DTOs.Post
{
    public class PostResponse : BaseDto
    {
        public PostResponse()
        {
            Comments = new HashSet<CommentResponse>();
        }
        public string Title { get; set; }
        public string PostDetail { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryResponse Category { get; set; }

        public Guid UserId { get; set; }
        public virtual UserResponse User { get; set; }

        public virtual ICollection<CommentResponse> Comments { get; set; }
    }
}
