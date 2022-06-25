using BilgeAdamBlog.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Entities
{
    public class Comment : CoreEntity
    {
        public string CommentText { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public User CreatedUserComment { get; set; }
        public User ModifiedUserComment { get; set; }
    }
}
