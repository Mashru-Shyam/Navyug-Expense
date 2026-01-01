using Application.Features.Auth.Commands.EmailVerification;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> RegisterUser(RegisterCommand cmd)
        {
            _logger.LogInformation("Backend Log: Initiating User Registration");
            var result = await _mediator.Send(cmd);

            return Ok(APIResponse.Ok(result));
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(EmailVerificationCommand cmd)
        {
            _logger.LogInformation("Backend Log: Verifying Email");
            var result = await _mediator.Send(cmd);

            return Ok(APIResponse.Ok(result));
        }
    }
}
