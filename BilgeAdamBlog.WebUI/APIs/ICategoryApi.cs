using BilgeAdamBlog.Common.DTOs.Category;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.APIs
{
    [Headers("Authorization: Bearer","Content-Type: application/json")]
    public interface ICategoryApi
    {
        [Get("/category")]
        Task<ApiResponse<List<CategoryResponse>>> List();

        [Get("/category/{id}")]
        Task<ApiResponse<CategoryResponse>> Get(Guid id);

        [Post("/category")]
        Task<ApiResponse<CategoryResponse>> Post(CategoryRequest request);

        [Put("/category/{id}")]
        Task<ApiResponse<CategoryResponse>> Put(Guid id, CategoryRequest request);

        [Delete("/category/{id}")]
        Task<ApiResponse<CategoryResponse>> Delete(Guid id);

        [Get("/category/activate/{id}")]
        Task<ApiResponse<CategoryResponse>> Activate(Guid id);

        [Get("/category/getactive")]
        Task<ApiResponse<List<CategoryResponse>>> Getactive();

    }
}
