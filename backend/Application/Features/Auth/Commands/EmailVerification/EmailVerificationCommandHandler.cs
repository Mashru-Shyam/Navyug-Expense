using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.EmailVerification
{
    public class EmailVerificationCommandHandler : IRequestHandler<EmailVerificationCommand, string>
    {
        private readonly IEmailVerificationTokenRepository _emailVerificationRepository;
        private readonly IUserRepository _userRepository;

        public EmailVerificationCommandHandler(IUserRepository userRepository, IEmailVerificationTokenRepository emailVerificationRepository)
        {
            _userRepository = userRepository;
            _emailVerificationRepository = emailVerificationRepository;
        }

        public async Task<string> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
        {
            var token = await _emailVerificationRepository.GetTokenAsync(request.Token, cancellationToken);
            if (token == null)
            {
                throw new NotFoundException("Invalid or expired verification token.");
            }

            if (token.ExpiresAt < DateTime.UtcNow)
            {
                await _emailVerificationRepository.DeleteAsync(token, cancellationToken);
                throw new ConflictException("Verification token has expired.");
            }

            var user = token.User;

            if(user.IsEmailConfirmed)
            {
                return "Email is already verified. You can sign in";
            }

            user.IsEmailConfirmed = true;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _emailVerificationRepository.DeleteAsync(token, cancellationToken);

            return "Email verified successfully. You can now sign in.";
        }
    }
}
