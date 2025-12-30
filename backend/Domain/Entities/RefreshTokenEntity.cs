using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid UserId {  get; set; }
        public UserEntity User { get; set; }
    }
}
