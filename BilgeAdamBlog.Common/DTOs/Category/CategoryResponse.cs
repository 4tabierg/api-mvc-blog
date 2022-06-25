using BilgeAdamBlog.Common.DTOs.Base;
using BilgeAdamBlog.Common.DTOs.Post;
using System;
using System.Collections.Generic;

namespace BilgeAdamBlog.Common.DTOs.Category
{
    public class CategoryResponse : BaseDto
    {
        public CategoryResponse()
        {
            Posts = new HashSet<PostResponse>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<PostResponse> Posts { get; set; }
    }
}
