using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmailVerificationTokenRepositroy : IEmailVerificationTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailVerificationTokenRepositroy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(EmailVerificationTokenEntity emailVerificationToken, CancellationToken cancellationToken)
        {
            await _context.EmailVerificationTokens.AddAsync(emailVerificationToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EmailVerificationTokenEntity emailVerificationToken, CancellationToken cancellationToken)
        {
            _context.EmailVerificationTokens.Remove(emailVerificationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<EmailVerificationTokenEntity?> GetTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.EmailVerificationTokens.Include(x => x.User).FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }
    }
}
