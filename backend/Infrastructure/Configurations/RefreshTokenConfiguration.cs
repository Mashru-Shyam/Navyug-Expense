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
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Id).ValueGeneratedOnAdd();
            builder.Property(rt => rt.Token).IsRequired();
            builder.Property(rt => rt.ExpiresAt).IsRequired();
            builder.Property(rt => rt.IsRevoked).HasDefaultValue(false);
            builder.Property(u => u.CreatedOn).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(rt => rt.Token).IsUnique();
            builder.HasIndex(rt => rt.UserId);
            builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
