using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExternalLoginEntity
    {
        public Guid Id { get; set; }
        public string Provider {  get; set; }
        public string ProvderUserId { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
