using Application.Common.Generators;
using Application.Common.Settings;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly RedirectionSettings _redirection;
        public RegisterCommandHandler(IUserRepository repository, IEmailService emailService, IEmailVerificationTokenRepository emailVerificationTokenRepository, IOptions<RedirectionSettings> redirection)
        {
            _repository = repository;
            _emailService = emailService;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _redirection = redirection.Value;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.IsEmailUniqueAsync(request.Email, cancellationToken))
            {
                throw new ConflictException("An account with this email address already exists. Please sign in or reset your password.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userEntity = new UserEntity
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                IsActive = true,
                IsEmailConfirmed = false,
                IsTwoFactorEnabled = false
            };

            await _repository.SaveAsync(userEntity, cancellationToken);

            var token = EmailTokenGenerator.Generate();

            var emailToken = new EmailVerificationTokenEntity
            {
                Token = token,
                UserId = userEntity.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };

            await _emailVerificationTokenRepository.CreateAsync(emailToken, cancellationToken);

            var verifyUrl =$"{_redirection.FrontendBaseUrl}/verify-email?token={token}";

            await _emailService.SendAsync(
                request.Email,
                "Welcome to NavyugWallet",
                $"<h1>Verify your email</h1><p>Click the link...<a href='{verifyUrl}'>Verify</a></p>",
                cancellationToken
            );

            return "Registration successful. Please check your email to verify your account.";
        }
    }
}
