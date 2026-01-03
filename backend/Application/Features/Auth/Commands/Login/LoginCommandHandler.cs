using Application.Common.Settings;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly JWTTokenSettings _jwtTokenSettings;
        private readonly IRefreshTokenRepository _refrshTokenRespository;

        public LoginCommandHandler(IJWTTokenService jwtTokenService, IUserRepository userRepository, IOptions<JWTTokenSettings> jwtTokenSettings, IRefreshTokenRepository refrshTokenRespository)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _jwtTokenSettings = jwtTokenSettings.Value;
            _refrshTokenRespository = refrshTokenRespository;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid Credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid credentials");
            }

            if (!user.IsEmailConfirmed)
            {
                throw new BadRequestException("Email not verified");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedException("User inactive");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            var refreshEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtTokenSettings.RefreshTokenDays),
                IsRevoked = false,
                CreatedOn = DateTime.UtcNow,
            };

            await _refrshTokenRespository.AddAsync(refreshEntity, cancellationToken);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                ExpiresIn = _jwtTokenSettings.AccessTokenMinutes * 60,
                RefreshToken = refreshEntity.Token,
                RefreshTokenExpiresAt = refreshEntity.ExpiresAt
            };

            return response;
        }
    }
}
