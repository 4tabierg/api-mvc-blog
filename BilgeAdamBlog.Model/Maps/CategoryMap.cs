using BilgeAdamBlog.Core.Map;
using BilgeAdamBlog.Model.Entities;
using BilgeAdamBlog.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBlog.Model.Maps
{
    public class CategoryMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasExtended();

                entity.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.Description).HasMaxLength(255).IsRequired(false);

                entity
                    .HasOne(c => c.CreatedUserCategory)
                    .WithMany(u => u.CreatedUserCategories)
                    .HasForeignKey(c => c.CreatedUserId);

                entity
                    .HasOne(c => c.ModifiedUserCategory)
                    .WithMany(u => u.ModifiedUserCategories)
                    .HasForeignKey(c => c.ModifiedUserId);
            });
        }
    }
}
