using Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
