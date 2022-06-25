using BilgeAdamBlog.Common.DTOs.Base;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.Common.DTOs.User;
using System;

namespace BilgeAdamBlog.Common.DTOs.Comment
{
    public class CommentResponse : BaseDto
    {
        public string CommentText { get; set; }
        public Guid PostId { get; set; }
        public virtual PostResponse Post { get; set; }
        public Guid UserId { get; set; }
        public virtual UserResponse User { get; set; }
    }
}
