using AutoMapper;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Areas.Admin.Models.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBlog.WebUI.Infrastructor.ViewComponents
{
    //Bir sınıf ViewComponent sınıfından türetildiğinde view component yapısı haline dönüşür.

    //Invoke metodu ViewComponent ile yapılacak işlemi belirlediğimiz metottur. Bir sayfadan bir ViewComponenti ismiyle çağırdığınızda bu yapı tetiklenir. Ardından Shared altında Components klasörü altında kendi ismiyle aynı klasörü arar ve o klasör içerisindeki Default.cshtml dosyasını çalıştırır. Bu yapı tıpkı PartialView kulllanımına benzemektedir.
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryViewComponent(
            ICategoryApi categoryApi,
            IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CategoryViewModel> vm = new List<CategoryViewModel>();
            var listModelResult = await _categoryApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                vm = _mapper.Map<List<CategoryViewModel>>(listModelResult.Content);
            return View(vm);
        }
    }
}
