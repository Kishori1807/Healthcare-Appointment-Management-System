using System.Text.Json;
using AppointmentService.Common;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred while processing request.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                ResourceNotFoundException => StatusCodes.Status404NotFound,
                DbUpdateException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var payload = new
            {
                success = false,
                statusCode = context.Response.StatusCode,
                message = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
