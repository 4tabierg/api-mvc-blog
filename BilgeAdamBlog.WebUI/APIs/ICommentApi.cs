using BilgeAdamBlog.Common.DTOs.Comment;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface ICommentApi
    {
        [Get("/comment")]
        Task<ApiResponse<List<CommentResponse>>> List();

        [Get("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Get(Guid id);

        [Post("/comment")]
        Task<ApiResponse<CommentResponse>> Post(CommentRequest request);

        [Put("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Put(Guid id, CommentRequest request);

        [Delete("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Delete(Guid id);

        [Get("/comment/activate/{id}")]
        Task<ApiResponse<CommentResponse>> Activate(Guid id);

        [Get("/comment/getactive")]
        Task<ApiResponse<List<CommentResponse>>> Getactive();
    }
}
