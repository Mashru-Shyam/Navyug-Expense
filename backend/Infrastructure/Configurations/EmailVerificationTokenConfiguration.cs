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
    public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationTokenEntity>
    {
        public void Configure (EntityTypeBuilder<EmailVerificationTokenEntity> builder)
        {
            builder.ToTable("EmailVerificationToken");
            builder.HasKey(evt => evt.Id);
            builder.Property(evt => evt.Id).ValueGeneratedOnAdd();
            builder.Property(evt => evt.Token).IsRequired();
            builder.Property(evt => evt.ExpiresAt).IsRequired();

            builder.HasIndex(evt => evt.Token).IsUnique();
            builder.HasIndex(evt => evt.UserId);
            builder.HasOne(evt => evt.User).WithMany(u => u.EmailVerificationTokens).HasForeignKey(evt => evt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
