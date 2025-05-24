using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace B3.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly Action<ILogger, Exception?> _logUnhandledException =
            LoggerMessage.Define(
                LogLevel.Error,
                new EventId(5000, nameof(ExceptionMiddleware)),
                "Erro não tratado.");

        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            HttpContext httpContext = context;

            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
            {
                _logUnhandledException(_logger, ex);
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Title = "Erro interno no servidor",
                Status = statusCode,
                Detail = _env.IsDevelopment()
                    ? exception.Message
                    : "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Instance = context.Request.Path
            };

            var json = JsonSerializer.Serialize(problemDetails, _jsonOptions);
            return context.Response.WriteAsync(json);
        }
    }
}
