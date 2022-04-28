using EcommerceStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.Infrastucture.Persistence.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => b.Name).IsUnique();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.FoundationYear).IsRequired();
        }
    }
}