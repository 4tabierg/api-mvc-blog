using BilgeAdamBlog.Common.DTOs.Base;
using System;

namespace BilgeAdamBlog.Common.DTOs.User
{
    public class UserRequest : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }
    }
}
