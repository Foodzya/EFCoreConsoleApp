using EcommerceStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.EntitiesConfigurations
{
    public class ProductCategorySectionConfiguration : IEntityTypeConfiguration<ProductCategorySection>
    {
        public void Configure(EntityTypeBuilder<ProductCategorySection> builder)
        {
            builder
                .HasKey(s => new { s.SectionId, s.ProductCategoryId });

            builder
                .HasOne(p => p.ProductCategory)
                .WithMany(s => s.ProductCategorySections)
                .HasForeignKey(p => p.ProductCategoryId);

            builder
                .HasOne(s => s.Section)
                .WithMany(p => p.ProductCategorySections)
                .HasForeignKey(s => s.SectionId);
        }
    }
}      