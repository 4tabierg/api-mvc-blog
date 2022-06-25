using BilgeAdamBlog.Core.Map;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Maps
{
    public class CommentMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasExtended();

                entity.Property(x => x.CommentText).IsRequired(true);

                entity
                    .HasOne(c => c.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(x => x.PostId);

                entity
                    .HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(x => x.UserId);

                entity
                    .HasOne(c => c.CreatedUserComment)
                    .WithMany(u => u.CreatedUserComments)
                    .HasForeignKey(c => c.CreatedUserId);

                entity
                    .HasOne(c => c.ModifiedUserComment)
                    .WithMany(u => u.ModifiedUserComments)
                    .HasForeignKey(c => c.ModifiedUserId);
            });
        }
    }
}
