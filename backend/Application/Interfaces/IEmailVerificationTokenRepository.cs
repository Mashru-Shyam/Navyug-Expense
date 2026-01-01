using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailVerificationTokenRepository
    {
        public Task CreateAsync(EmailVerificationTokenEntity emailVerificationToken, CancellationToken cancellationToken);
        public Task<EmailVerificationTokenEntity?> GetTokenAsync(string token, CancellationToken cancellationToken);
        public Task DeleteAsync(EmailVerificationTokenEntity emailVerificationToken, CancellationToken cancellationToken);
    }
}
