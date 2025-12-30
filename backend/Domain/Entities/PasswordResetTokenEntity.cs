using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PasswordResetTokenEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
