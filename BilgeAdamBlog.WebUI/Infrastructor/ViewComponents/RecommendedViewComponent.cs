using AutoMapper;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Infrastructor.ViewComponents
{
    public class RecommendedViewComponent : ViewComponent
    {
        private readonly IPostApi _postApi;
        private readonly IMapper _mapper;

        public RecommendedViewComponent(
            IPostApi postApi,
            IMapper mapper)
        {
            _postApi = postApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            var listModelResult = await _postApi.GetActive();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                posts = _mapper.Map<List<PostViewModel>>(listModelResult.Content.OrderBy(x => Guid.NewGuid()).Take(3).ToList());
            return View(posts);
        }
    }
}