using BilgeAdamBlog.Core.Entity;
using System.Collections.Generic;

namespace BilgeAdamBlog.Model.Entities
{
    public class Category : CoreEntity
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public User CreatedUserCategory { get; set; }
        public User ModifiedUserCategory { get; set; }
    }
}
