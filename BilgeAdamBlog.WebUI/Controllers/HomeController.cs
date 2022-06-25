using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostApi _postApi;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        public HomeController(
            IPostApi postApi, 
            IUserApi userApi, 
            IMapper mapper)
        {
            _postApi = postApi;
            _userApi = userApi;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            var postResult = await _postApi.GetActive();
            if (postResult.IsSuccessStatusCode && postResult.Content.Any())
                posts = _mapper.Map<List<PostViewModel>>(postResult.Content.OrderByDescending(x => x.ViewCount).Take(5).ToList());
            return View(posts);
        }

        public async Task<IActionResult> Post(Guid id)
        {
            var getPostResult = await _postApi.Get(id);
            if (getPostResult.IsSuccessStatusCode && getPostResult.Content != null)
            {
                PostViewModel x = _mapper.Map<PostViewModel>(getPostResult.Content);
                x.ViewCount++;
                var updateResult = _postApi.Put(x.Id, _mapper.Map<PostRequest>(x));

                UserViewModel userViewModel = _mapper.Map<UserViewModel>((await _userApi.Get(x.UserId)).Content);
                return View(Tuple.Create<PostViewModel, UserViewModel>(x, userViewModel));
            }
            return View();
        }

        public async Task<IActionResult> Posts(Guid id)
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            var postResult = await _postApi.GetPostsByCategoryId(id);
            if (postResult.IsSuccessStatusCode && postResult.Content.Any())
                posts = _mapper.Map<List<PostViewModel>>(postResult.Content);
            return View(posts);
        }
    }
}
