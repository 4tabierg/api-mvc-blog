using BilgeAdamBlog.Core.Map;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Maps
{
    public class UserMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasExtended();

                entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.LastName).HasMaxLength(150).IsRequired(true);
                entity.Property(x => x.Title).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.ImageUrl).HasMaxLength(250).IsRequired(false);
                entity.Property(x => x.Email).HasMaxLength(150).IsRequired(true);
                entity.Property(x => x.Password).HasMaxLength(12).IsRequired(true);
                entity.Property(x => x.LastLogin).IsRequired(false);
                entity.Property(x => x.LastIPAdress).HasMaxLength(20).IsRequired(false);

                entity
                    .HasOne(c => c.CreatedUser)
                    .WithMany(u => u.CreatedUsers)
                    .HasForeignKey(c => c.CreatedUserId);

                entity
                    .HasOne(c => c.ModifiedUser)
                    .WithMany(u => u.ModifiedUsers)
                    .HasForeignKey(c => c.ModifiedUserId);

            });
        }
    }
}
