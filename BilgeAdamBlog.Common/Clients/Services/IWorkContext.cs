using BilgeAdamBlog.Common.DTOs.User;

namespace BilgeAdamBlog.Common.Clients.Services
{
    public interface IWorkContext
    {
        UserResponse CurrentUser { get; set; }
    }
}
