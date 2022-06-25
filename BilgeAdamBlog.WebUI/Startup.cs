using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.WebUI.APIs;
using BilgeAdamBlog.WebUI.Infrastructor.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Refit;

namespace BilgeAdamBlog.WebUI
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            //Add HttpContextAccessor
            services.AddHttpContextAccessor();

            //MVC modülünü sürecimize ekliyoruz.
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Register Refit Interfaces
            RegisterClients(services);

            //Asp.Net Core MVC'de Oturum yönetimini Core Identity ile gerçekleþtiriyoruz. Core Identity'de Cookie yönetimini kullanýyoruz.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            //Net Core yapýsý tamamýyla Dependency Injection yapýsýyla çalýþtýðýndan Interface ile Service class'larýnýn baðýmlýlýðýný tanýmlýyoruz.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Geliþtirme Sürecinde
            if (env.IsDevelopment())
            {
                //Hata Sayfalarýný görmek istediðimizden dolayý aþaðýdaki servisi aktifleþtiriyoruz....
                app.UseDeveloperExceptionPage();
            }

            //Core Identity Servisimizi aktifleþtiriyoruz...
            app.UseAuthentication();

            //wwwroot klasörüne eriþim izni veriyoruz...
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterClients(IServiceCollection services)
        {
            //Register Http Handler
            services.AddScoped<AuthTokenHandler>();

            var baseUrl = Configuration
                .GetSection("Settings")
                .GetSection("Host")["CoreServer"];
            var baseUri = new Uri(baseUrl);

            //Account 
            services.AddRefitClient<IAccountApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

            //Category 
            services.AddRefitClient<ICategoryApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s)=> s.GetService<AuthTokenHandler>());

            //Post 
            services.AddRefitClient<IPostApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //User 
            services.AddRefitClient<IUserApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

            //Comment 
            services.AddRefitClient<ICommentApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = baseUri; })
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60)))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddHttpMessageHandler((s) => s.GetService<AuthTokenHandler>());

        }
    }
}
