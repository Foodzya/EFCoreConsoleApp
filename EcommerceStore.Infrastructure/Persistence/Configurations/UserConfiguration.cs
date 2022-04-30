using EcommerceStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.Infrastucture.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.RoleId).IsUnique(false);
            builder
                .HasOne(u => u.Role)
                .WithOne(r => r.User);
        }
    }
}