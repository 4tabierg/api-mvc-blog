using BilgeAdamBlog.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeAdamBlog.Model.Maps.Base
{
    public static class EntityBuilderExtensions
    {
        public static void HasExtended<T>(this EntityTypeBuilder<T> entity) where T : CoreEntity
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Status).IsRequired(true);

            entity.Property(x => x.CreatedDate).IsRequired(false);
            entity.Property(x => x.CreatedComputerName).HasMaxLength(255).IsRequired(false);
            entity.Property(x => x.CreatedIP).HasMaxLength(20).IsRequired(false);
            entity.Property(x => x.CreatedUserId).IsRequired(false);

            entity.Property(x => x.ModifiedDate).IsRequired(false);
            entity.Property(x => x.ModifiedComputerName).HasMaxLength(255).IsRequired(false);
            entity.Property(x => x.ModifiedIP).HasMaxLength(20).IsRequired(false);
            entity.Property(x => x.ModifiedUserId).IsRequired(false);
        }
    }
}
