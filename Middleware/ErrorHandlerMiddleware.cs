using System.Net;
using System.Text.Json;

namespace LibrosApi.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            _logger.LogError(ex, "Error no manejado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            KeyNotFoundException => new
            {
                statusCode = (int)HttpStatusCode.NotFound,
                message = "Recurso no encontrado",
                details = exception.Message
            },
            ArgumentException => new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                message = "Solicitud inválida",
                details = exception.Message
            },
            _ => new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                message = "Error interno del servidor",
                details = "Ocurrió un error inesperado. Por favor contacte al administrador."
            }
        };

        context.Response.StatusCode = response.statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}