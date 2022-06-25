using BilgeAdamBlog.Common.Clients.Models;
using BilgeAdamBlog.Common.DTOs.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.APIs
{
    [Headers("Content-Type: application/json")]
    public interface IAccountApi
    {
        [Get("/account/login")]
        Task<ApiResponse<WebApiResponse<UserResponse>>> Login([Query]UserRequest request);
    }
}
