using Application.Exceptions;
using Shared.Responses;

namespace API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ConflictException cex)
            {
                _logger.LogInformation(cex.Message);
                context.Response.StatusCode = 409;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    APIResponse.Fail(cex.Message));
            }
            catch (NotFoundException nfe)
            {
                _logger.LogInformation(nfe.Message);
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    APIResponse.Fail(nfe.Message));
            }
            catch (UnauthorizedException ue)
            {
                _logger.LogInformation(ue.Message);
                context.Response.StatusCode = 40;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    APIResponse.Fail(ue.Message));
            }
            catch (BadRequestException bre)
            {
                _logger.LogInformation(bre.Message);
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    APIResponse.Fail(bre.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Backend Log : Global Exception occured");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    APIResponse.Fail(ex.Message));
            }
        }
    }
}
