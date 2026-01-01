using Application.Features.Auth.Commands.Register;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        public Task SaveAsync(UserEntity user, CancellationToken cancellationToken);
        public Task<bool> IsEmailUniqueAsync (string? email, CancellationToken cancellationToken);
        public Task UpdateAsync(UserEntity user, CancellationToken cancellationToken);
    }
}
