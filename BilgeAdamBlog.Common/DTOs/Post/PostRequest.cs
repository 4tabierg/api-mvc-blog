using BilgeAdamBlog.Common.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Common.DTOs.Post
{
    public class PostRequest : BaseDto
    {
        public string Title { get; set; }
        public string PostDetail { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
