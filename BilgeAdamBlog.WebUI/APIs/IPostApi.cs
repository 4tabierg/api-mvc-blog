using BilgeAdamBlog.Common.DTOs.Post;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IPostApi
    {
        [Get("/post")]
        Task<ApiResponse<List<PostResponse>>> List();

        [Get("/post/{id}")]
        Task<ApiResponse<PostResponse>> Get(Guid id);

        [Post("/post")]
        Task<ApiResponse<PostResponse>> Post(PostRequest request);

        [Put("/post/{id}")]
        Task<ApiResponse<PostResponse>> Put(Guid id, PostRequest request);

        [Delete("/post/{id}")]
        Task<ApiResponse<PostResponse>> Delete(Guid id);

        [Get("/post/activate/{id}")]
        Task<ApiResponse<PostResponse>> Activate(Guid id);

        [Get("/post/getactive")]
        Task<ApiResponse<List<PostResponse>>> GetActive();

        [Get("/post/GetByCategoryId/{categoryId}")]
        Task<ApiResponse<List<PostResponse>>> GetPostsByCategoryId(Guid categoryId);
    }
}
