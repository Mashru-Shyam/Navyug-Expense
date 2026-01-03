using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task AddAsync(RefreshTokenEntity refreshToken, CancellationToken cancellationToken);
        public Task<RefreshTokenEntity?> GetAsync(string refreshToken, CancellationToken cancellation);
        public Task UpdateAsync(RefreshTokenEntity refreshToken, CancellationToken cancellationToken);
    }
}
