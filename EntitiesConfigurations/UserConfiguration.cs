using EcommerceStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder
                .HasOne(u => u.Role)
                .WithOne(r => r.User)
                .HasForeignKey<User>(r => r.RoleId);
        }
    }
}