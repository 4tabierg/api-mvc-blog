using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.Common.DTOs.Post;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.PostViewModels;
using BilgeAdamBlog.WebUI.Infrastructor.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IPostApi _postApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public PostController(
            IWebHostEnvironment env,
            IPostApi postApi,
            ICategoryApi categoryApi,
            IMapper mapper)
        {
            _env = env;
            _postApi = postApi;
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PostViewModel> vm = new List<PostViewModel>();
            var listModelResult = await _postApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                vm = _mapper.Map<List<PostViewModel>>(listModelResult.Content);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            List<CategoryResponse> model = new List<CategoryResponse>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = listModelResult.Content;
            ViewBag.Categories = new SelectList(model, "Id", "CategoryName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreatePostViewModel item, List<IFormFile> files)
        {
            item.UserId = Guid.Parse(User.Claims?.FirstOrDefault(x => x.Type == "Id").Value);
            if (ModelState.IsValid)
            {
                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _env, out imgResult);
                if (imgResult)
                    item.ImagePath = imgPath;
                else
                {
                    ViewBag.Message = imgPath;
                    return View();
                }

                var insertResult = await _postApi.Post(_mapper.Map<PostRequest>(item));
                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi sırasında bir hata oluştu. Lütfen tüm alanları kontrol edip tekrar deneyiniz..";
            }
            else
                TempData["Message"] = "İşlem başarısız oldu. Lütfen tüm alanları kontrol edip tekrar deneyiniz..";
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            List<CategoryResponse> model = new List<CategoryResponse>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = listModelResult.Content;
            ViewBag.Categories = new SelectList(model, "Id", "CategoryName");

            UpdatePostViewModel updateModel = new UpdatePostViewModel();
            var updateModelResult = await _postApi.Get(id);
            if (updateModelResult.IsSuccessStatusCode || updateModelResult.Content != null)
                updateModel = _mapper.Map<UpdatePostViewModel>(updateModelResult.Content);
            return View(updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostViewModel item)
        {
            item.UserId = Guid.Parse(User.Claims?.FirstOrDefault(x => x.Type == "Id").Value);
            if (ModelState.IsValid)
            {
                var updateResult = await _postApi.Put(item.Id, _mapper.Map<PostRequest>(item));
                if (updateResult.IsSuccessStatusCode || updateResult.Content != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm alanları kontrol edip tekrar deneyiniz..";
            }
            else
                TempData["Message"] = "İşlem başarısız oldu. Lütfen tüm alanları kontrol edip tekrar deneyiniz..";
            return View(item);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _postApi.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _postApi.Activate(id);
            return RedirectToAction("Index");
        }
    }
}
