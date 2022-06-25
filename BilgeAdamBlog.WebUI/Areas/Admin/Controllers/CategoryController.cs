using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.DTOs.Category;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryController(
            IWebHostEnvironment env,
            ICategoryApi categoryApi,
            IMapper mapper
            )
        {
            _env = env;
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> vm = new List<CategoryViewModel>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                vm = _mapper.Map<List<CategoryViewModel>>(listModelResult.Content);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreateCategoryViewModel item)
        {
            if (ModelState.IsValid)
            {
                var insertResult = await _categoryApi.Post(_mapper.Map<CategoryRequest>(item));
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
            UpdateCategoryViewModel model = new UpdateCategoryViewModel();
            var updateModelResult = await _categoryApi.Get(id);
            if (updateModelResult.IsSuccessStatusCode || updateModelResult.Content != null)
                model = _mapper.Map<UpdateCategoryViewModel>(updateModelResult.Content);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel item)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _categoryApi.Put(item.Id, _mapper.Map<CategoryRequest>(item));
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
            var deleteResult = await _categoryApi.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _categoryApi.Activate(id);
            return RedirectToAction("Index");
        }
    }
}
