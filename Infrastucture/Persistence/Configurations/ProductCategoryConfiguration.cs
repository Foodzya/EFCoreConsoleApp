using EcommerceStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.Infrastucture.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ParentCategoryId).IsRequired(false);
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildrenCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false);
        }
    }
}