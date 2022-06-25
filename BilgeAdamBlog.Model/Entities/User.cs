using BilgeAdamBlog.Core.Entity;
using System;
using System.Collections.Generic;

namespace BilgeAdamBlog.Model.Entities
{
    public class User : CoreEntity
    {
        public User()
        {
            ModifiedUsers = new HashSet<User>();
            CreatedUsers = new HashSet<User>();
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            CreatedUserCategories = new HashSet<Category>();
            ModifiedUserCategories = new HashSet<Category>();
            CreatedUserPosts = new HashSet<Post>();
            ModifiedUserPosts = new HashSet<Post>();
            CreatedUserComments = new HashSet<Comment>();
            ModifiedUserComments = new HashSet<Comment>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }

        public User CreatedUser { get; set; }
        public User ModifiedUser { get; set; }
        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> ModifiedUsers { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Category> CreatedUserCategories { get; set; }
        public virtual ICollection<Category> ModifiedUserCategories { get; set; }
        public virtual ICollection<Post> CreatedUserPosts { get; set; }
        public virtual ICollection<Post> ModifiedUserPosts { get; set; }
        public virtual ICollection<Comment> CreatedUserComments { get; set; }
        public virtual ICollection<Comment> ModifiedUserComments { get; set; }

    }
}
