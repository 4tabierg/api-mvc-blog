using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBlog.WebUI.Areas.Admin.Controllers
{
    //Authorize attribute'unu kullanarak bu sayfaya sadece CoreIdentity yapımızdan geçmiş kullanıcıların ulaşabileceğini belirtiyoruz.
    //Bir area oluşturduğunuzda bu area içerisine eklediğiniz controller dosyaları Net Framework'de ki gibi buraya ait olmuyor, Her bir controller'ın üzerine hangi area'ya ait olduğunu Area Attribute'u ile belirtmek zorundayız.
    [Area("Admin"),Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
