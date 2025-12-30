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
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetTokenEntity>
    {
        public void Configure (EntityTypeBuilder<PasswordResetTokenEntity> builder)
        {
            builder.ToTable("PasswordResetTokens");
            builder.HasKey(prt => prt.Id);
            builder.Property(prt => prt.Id).ValueGeneratedOnAdd();
            builder.Property(prt => prt.Token).IsRequired();
            builder.Property(prt => prt.ExpiresAt).IsRequired();

            builder.HasIndex(prt => prt.Token).IsUnique();
            builder.HasIndex(prt => prt.UserId);
            builder.HasOne(prt => prt.User).WithMany(u => u.PasswordResetTokens).HasForeignKey(prt => prt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
