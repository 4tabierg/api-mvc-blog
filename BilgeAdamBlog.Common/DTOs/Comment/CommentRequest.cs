using BilgeAdamBlog.Common.DTOs.Base;
using System;

namespace BilgeAdamBlog.Common.DTOs.Comment
{
    public class CommentRequest : BaseDto
    {
        public string CommentText { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
