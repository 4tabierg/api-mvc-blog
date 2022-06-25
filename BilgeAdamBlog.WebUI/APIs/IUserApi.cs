using BilgeAdamBlog.Common.DTOs.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IUserApi
    {
        [Get("/user")]
        Task<ApiResponse<List<UserResponse>>> List();

        [Get("/user/{id}")]
        Task<ApiResponse<UserResponse>> Get(Guid id);

        [Post("/user")]
        Task<ApiResponse<UserResponse>> Post(UserRequest request);

        [Put("/user/{id}")]
        Task<ApiResponse<UserResponse>> Put(Guid id, UserRequest request);

        [Delete("/user/{id}")]
        Task<ApiResponse<UserResponse>> Delete(Guid id);

        [Get("/user/activate/{id}")]
        Task<ApiResponse<UserResponse>> Activate(Guid id);

        [Get("/user/getactive")]
        Task<ApiResponse<List<UserResponse>>> Getactive();
    }
}
