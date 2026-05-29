using System.Net;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NamazTimeApp.Core;
using NamazTimeApp.Core.Infrastructure;

namespace NamazTimeApp.Application.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var response = new Response<object> { Model = null! };
        var root = GetRootException(exception);
        var errorId = Guid.NewGuid().ToString();

        var statusCode = exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            DomainException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            DbUpdateException => HttpStatusCode.BadRequest,
            PostgresException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        _logger.LogError(exception, "ErrorId: {ErrorId} | Exception occurred", errorId);

        if (_env.IsDevelopment())
        {
            response.Messages.Add(CreateMessage(MessageType.Error, root.Message));
            response.Messages.Add(CreateMessage(MessageType.Information, root.StackTrace ?? "No stack trace"));
        }
        else
        {
            response.Messages.Add(CreateMessage(MessageType.Error, Constants.ApplicationMessages.GENERIC_ERROR));
            response.Messages.Add(CreateMessage(MessageType.Information, $"ErrorId: {errorId}"));
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }

    private static Message CreateMessage(MessageType type, string value)
    {
        return new Message
        {
            MessageType = type,
            Value = value
        };
    }

    private static Exception GetRootException(Exception ex)
    {
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }

        return ex;
    }
}
