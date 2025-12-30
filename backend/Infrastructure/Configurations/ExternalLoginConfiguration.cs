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
    public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLoginEntity>
    {
        public void Configure(EntityTypeBuilder<ExternalLoginEntity> builder)
        {
            builder.ToTable("ExternalLogins");
            builder.HasKey(el => el.Id);
            builder.Property(el => el.Id).ValueGeneratedOnAdd();
            builder.Property(el => el.Provider).IsRequired().HasMaxLength(128);
            builder.Property(el => el.ProvderUserId).IsRequired().HasMaxLength(128);

            builder.HasIndex(el => el.UserId);
            builder.HasOne(el => el.User).WithMany(u => u.ExternalLogins).HasForeignKey(el => el.UserId);
        }
    }
}
