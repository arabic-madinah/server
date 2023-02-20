using System.Net;
using System.Text.Json;

namespace MadinahArabic.MiddleWare;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unhandled exception occurred while processing the request: {ex.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var errorMessage = new
            {
                message = "An error occurred while processing your request. Please try again later.",
                error = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
        }
    }
}