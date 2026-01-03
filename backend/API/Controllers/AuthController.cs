using Application.Features.Auth.Commands.EmailVerification;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterCommand cmd, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backend Log: Initiating User Registration");
            var result = await _mediator.Send(cmd, cancellationToken);

            return Ok(APIResponse.Ok(result));
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(EmailVerificationCommand cmd, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backend Log: Verifying Email");
            var result = await _mediator.Send(cmd, cancellationToken);

            return Ok(APIResponse.Ok(result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginCommand cmd, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backend Log: Login User");
            var result = await _mediator.Send(cmd, cancellationToken); 

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.RefreshTokenExpiresAt
            });

            return Ok(APIResponse.Ok(new { result.AccessToken, result.ExpiresIn }));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                return Unauthorized("Login Again");

            var cmd = new RefreshTokenCommand { RefreshToken = refreshToken };

            var result = await _mediator.Send(cmd);

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.RefreshTokenExpiresAt
            });

            return Ok(APIResponse.Ok(new { result.AccessToken, result.ExpiresIn }));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var token))
            {
                var cmd = new LogoutCommand { RefreshToken =  token };
                await _mediator.Send(cmd, cancellationToken);
            }

            Response.Cookies.Delete("refreshToken");
            return Ok(APIResponse.Ok("Logout Success"));
        }
    }
}
