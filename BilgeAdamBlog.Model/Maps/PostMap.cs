using BilgeAdamBlog.Core.Map;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Maps
{
    public class PostMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Post>(entity => 
            {
                entity.ToTable("Posts");

                entity.HasExtended();

                entity.Property(x => x.Title).HasMaxLength(200).IsRequired(true);
                entity.Property(x => x.PostDetail).IsRequired(true);
                entity.Property(x => x.Tags).IsRequired(true);
                entity.Property(x => x.ImagePath).IsRequired(true);

                entity
                    .HasOne(c => c.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(x => x.CategoryId);

                entity
                    .HasOne(c => c.User)
                    .WithMany(u => u.Posts)
                    .HasForeignKey(x => x.UserId);

                entity
                    .HasOne(c => c.CreatedUserPost)
                    .WithMany(u => u.CreatedUserPosts)
                    .HasForeignKey(c => c.CreatedUserId);

                entity
                    .HasOne(c => c.ModifiedUserPost)
                    .WithMany(u => u.ModifiedUserPosts)
                    .HasForeignKey(c => c.ModifiedUserId);

            });
        }
    }
}
