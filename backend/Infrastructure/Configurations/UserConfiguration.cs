using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256).IsUnicode(true);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
            builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(false);
            builder.Property(u => u.IsEmailConfirmed).IsRequired().HasDefaultValue(false);
            builder.Property(u => u.IsTwoFactorEnabled).IsRequired().HasDefaultValue(false);
            builder.Property(u => u.CreateBy).HasMaxLength(256);
            builder.Property(u => u.CreatedOn).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(u => u.UpdatedBy).HasMaxLength(256);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
