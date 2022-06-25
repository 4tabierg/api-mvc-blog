using BilgeAdamBlog.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Entities
{
    public class Post : CoreEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
        public string Title { get; set; }
        public string PostDetail { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public User CreatedUserPost { get; set; }
        public User ModifiedUserPost { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
