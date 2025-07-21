using System.Text.Json;
using Presentation.Models.Responses.Base;

namespace Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            var response = new ExecutionRes();
            response.Error = "Something went wrong. Please try again later";
            response.ErrorCode = "Exception";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}