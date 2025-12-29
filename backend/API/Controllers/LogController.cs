using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost("frontend")]
        public IActionResult LogFromFrontend(LogRequest request)
        {
            switch(request.Level.ToLower())
            {
                case "information":
                case "info":
                    _logger.LogInformation("Frontend Log: {Message} | Data: {Data} | CorrelationId: {CorrelationId}", request.Message, request.Data, request.CorrelationId);
                    break;
                case "warning":
                case "warn":
                    _logger.LogWarning("Frontend Log: {Message} | Data: {Data} | CorrelationId: {CorrelationId}", request.Message, request.Data, request.CorrelationId);
                    break;
                case "error":
                    _logger.LogError("Frontend Log: {Message} | Data: {Data} | CorrelationId: {CorrelationId}", request.Message, request.Data, request.CorrelationId);
                    break;
                default:
                    _logger.LogInformation("Frontend Log (Unknown Level): {Message} | Data: {Data} | CorrelationId: {CorrelationId}", request.Message, request.Data, request.CorrelationId);
                    break;
            }

            return Ok(APIResponse.Ok("Log Added",request.CorrelationId));
        }
    }
}
