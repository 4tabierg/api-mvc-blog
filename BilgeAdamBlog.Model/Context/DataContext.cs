using BilgeAdamBlog.Core.Entity;
using BilgeAdamBlog.Core.Map;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Model.Maps;
using BilgeAdamBlog.Model.SeedData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BilgeAdamBlog.Model.Context
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RegisterMapping(modelBuilder);

            //SeedData'ları tetikliyoruz
            modelBuilder.ApplyConfiguration(new UserSeedData());
            
        }

        private void RegisterMapping(ModelBuilder modelBuilder)
        {
            var typeToRegister = new List<Type>();
            var dataAssembly = Assembly.GetExecutingAssembly();

            typeToRegister.AddRange(dataAssembly.DefinedTypes.Select(x => x.AsType()));
            foreach (var builderType in typeToRegister.Where(x=> typeof(IEntityBuilder).IsAssignableFrom(x)))
            {
                if (builderType != null && builderType != typeof(IEntityBuilder))
                {
                    var builder = (IEntityBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string computerName = Environment.MachineName;
            string IPAdress = "127.0.0.1";
            DateTime date = DateTime.Now;

            foreach (var item in modifiedEntities)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = IPAdress;
                            entity.CreatedDate = date;
                            entity.CreatedUserId = GetUserId();
                            break;
                        case EntityState.Modified:
                            entity.ModifiedComputerName = computerName;
                            entity.ModifiedIP = IPAdress;
                            entity.ModifiedDate = date;
                            entity.ModifiedUserId = GetUserId();
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private Guid? GetUserId()
        {
            string userId = "";
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
                userId = claims?.FirstOrDefault(x => x.Type.Equals("jti", StringComparison.OrdinalIgnoreCase))?.Value;
            }
            if (userId != null)
                return Guid.Parse(userId);
            else
                return Guid.Empty;
        }
    }
}
