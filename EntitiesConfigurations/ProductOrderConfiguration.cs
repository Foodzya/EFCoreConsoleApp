using EcommerceStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.EntitiesConfigurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.Price).IsRequired();
            builder
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(p => p.ProductId);
            builder
                .HasOne(o => o.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(o => o.OrderId);
        }
    }
}      