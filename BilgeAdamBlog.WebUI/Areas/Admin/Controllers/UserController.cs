using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.UserViewModels;
using BilgeAdamBlog.WebUI.Infrastructor.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;

        public UserController(
            IWebHostEnvironment env,
            IUserApi userApi,
            IMapper mapper
            )
        {
            _env = env;
            _userApi = userApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> vm = new List<UserViewModel>();
            var listModelResult = await _userApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                vm = _mapper.Map<List<UserViewModel>>(listModelResult.Content);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreateUserViewModel item, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _env, out imgResult);
                if (imgResult)
                    item.ImageUrl = imgPath;
                else
                {
                    ViewBag.Message = imgPath;
                    return View();
                }

                var insertResult = await _userApi.Post(_mapper.Map<UserRequest>(item));
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
            UpdateUserViewModel model = new UpdateUserViewModel();
            var updateModelResult = await _userApi.Get(id);
            if (updateModelResult.IsSuccessStatusCode || updateModelResult.Content != null)
                model = _mapper.Map<UpdateUserViewModel>(updateModelResult.Content);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel item)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _userApi.Put(item.Id, _mapper.Map<UserRequest>(item));
                if (updateResult.IsSuccessStatusCode || updateResult.Content != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm alalnları kontrol edip tekrar deneyiniz..";
            }
            else
                TempData["Message"] = "İşlem başarısız oldu. Lütfen tüm alalnları kontrol edip tekrar deneyiniz..";
            return View(item);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _userApi.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _userApi.Activate(id);
            return RedirectToAction("Index");
        }
    }
}
