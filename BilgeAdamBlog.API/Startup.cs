using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBlog.API.Infrastructor.Models.Base;
using BilgeAdamBlog.Common.Clients.Services;
using BilgeAdamBlog.Model.Context;
using BilgeAdamBlog.Service.Service.Category;
using BilgeAdamBlog.Service.Service.Comment;
using BilgeAdamBlog.Service.Service.Post;
using BilgeAdamBlog.Service.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BilgeAdamBlog.API
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

            //API modülünü sürecimize ekliyoruz.
            services.AddControllers();

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //DataContext dosyasýna gönderilecek olan Postgre Sql connection string yapýmýzý oluþturuyoruz.
            services.AddDbContext<DataContext>(option =>
            {
                option.UseNpgsql(Configuration["ConnectionStrings:Conn"]);
                //EF nesnelerinin EF tarafýndan izlenmesini iptal etmek için aþaðýdaki satýrý ekliyoruz...
                //option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Net Core yapýsý tamamýyla Dependency Injection yapýsýyla çalýþtýðýndan Interface ile Service class'larýnýn baðýmlýlýðýný tanýmlýyoruz.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IWorkContext, ApiWorkContext>();

            //Add Auth
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger on ASP.NET Core",
                    Version = "1.0.0",
                    Description = "Bilge Adam Backeng Servis Projesi(ASP.NET Core 3.1)",
                    TermsOfService = new Uri("http://swagger.io/terms")
                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Bilge Adam Core API Projesi JWT Authorization header (Bearer) kullanmaktadýr. Örnek: \"Authorization: Bearer {token}\"",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());

                options.AddPolicy("AllowWithOrigins",
                    b => b.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "MyAPI V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
