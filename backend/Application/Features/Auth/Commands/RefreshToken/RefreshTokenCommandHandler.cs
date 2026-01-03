using Application.Common.Settings;
using Application.Exceptions;
using Application.Features.Auth.Commands.Login;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshToken
{
    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IRefreshTokenRepository _refrshTokenRespository;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly JWTTokenSettings _jwtTokenSettings;

        public RefreshTokenCommandHandler(IRefreshTokenRepository refrshTokenRespository, IOptions<JWTTokenSettings> jwtSettings, IJWTTokenService jwtTokenService)
        {
            _refrshTokenRespository = refrshTokenRespository;
            _jwtTokenSettings = jwtSettings.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refrshTokenRespository.GetAsync(request.RefreshToken, cancellationToken);

            if (refreshToken == null || refreshToken.IsRevoked ||refreshToken.ExpiresAt <= DateTime.UtcNow || !refreshToken.User.IsActive)
            {
                throw new UnauthorizedException("Invalid Credentials");
            }

            refreshToken.IsRevoked = true;

            var accessToken = _jwtTokenService.GenerateAccessToken(refreshToken.User);
            var newrefreshToken = _jwtTokenService.GenerateRefreshToken();

            var refreshEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = refreshToken.UserId,
                Token = newrefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtTokenSettings.RefreshTokenDays),
                IsRevoked = false,
                CreatedOn = DateTime.UtcNow
            };

            await _refrshTokenRespository.AddAsync(refreshEntity, cancellationToken);

            var response = new RefreshTokenResponse
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
