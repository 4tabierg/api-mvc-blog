using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.Common.Clients.Models;
using BilgeAdamBlog.Common.DTOs.User;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountApi accountApi,
            IMapper mapper)
        {
            _accountApi = accountApi;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _accountApi.Login(_mapper.Map<UserRequest>(request));
                if (loginResult.IsSuccessStatusCode && loginResult.Content.IsSuccess)
                {
                    UserResponse user = loginResult.Content.ResulData;
                    var claims = new List<Claim>()
                    {
                        new Claim("Id",user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.FirstName),
                        new Claim(ClaimTypes.Surname,user.LastName),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim("Image",user.ImageUrl)
                    };
                    //Giriş işlemlerini tamamlıyoruz ve kullanıcıyı yönetici sayfasına yönlendiriyoruz...
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    HttpContext.Response.Cookies.Append("BilgeAdamAccessToken", user.AccessToken.AccessToken);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return View(request);
        }

        //Kullanıcı çıkış işlemlerini gerçekleştiriyoruz...
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
