using BilgeAdamBlog.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BilgeAdamBlog.Model.SeedData
{
    public class UserSeedData : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Status = Core.Entity.Enums.Status.Active,
                    Email = "admin@admin.com",
                    Password = "123",
                    FirstName ="Admin",
                    LastName = "Admin",
                    Title = "Admin",
                    ImageUrl = "/",
                    LastLogin = DateTime.Now,
                    LastIPAdress = "127.0.0.1"
                });
        }
    }
}
