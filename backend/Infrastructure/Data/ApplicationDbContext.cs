using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<UserRoleEntity> UserRoles { get; set; }
        DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        DbSet<EmailVerificationTokenEntity> EmailVerificationTokens{ get; set; }
        DbSet<PasswordResetTokenEntity> PasswordResetTokens{ get; set; }
        DbSet<ExternalLoginEntity> ExternalLogins{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
