
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebLayer.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred. Path: {Path}", context.Request.Path);

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the global exception handler will not modify the response.");
                    throw;
                }

                await HandleExceptionsAsync(context, ex);
            }
        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ItemNotFoundException ex:
                    await WriteResponseAsync(context, StatusCodes.Status404NotFound, ex.Code, ex.Message);
                    break;

                case PermissionDeniedException ex:
                    await WriteResponseAsync(context, StatusCodes.Status403Forbidden, ex.Code, ex.Message);
                    break;

                case DuplicateUserException ex:
                    await WriteResponseAsync(context, StatusCodes.Status409Conflict, ex.Code, ex.Message);
                    break;

                case AuthenticationException ex:
                    await WriteResponseAsync(context, StatusCodes.Status401Unauthorized, "AuthenticationError_401", ex.Message);
                    break;

                case UnauthorizedAccessException ex:
                    await WriteResponseAsync(context, StatusCodes.Status401Unauthorized, "Unauthorized_401", ex.Message);
                    break;

                case ArgumentException ex:
                    await WriteResponseAsync(context, StatusCodes.Status400BadRequest, "BadRequest_400", ex.Message);
                    break;

                case BaseBussinessException ex:
                    await WriteResponseAsync(context, StatusCodes.Status400BadRequest, ex.Code, ex.Message);
                    break;

                case BaseException ex:
                    await WriteResponseAsync(context, StatusCodes.Status400BadRequest, ex.Code, ex.Message);
                    break;

                case DbUpdateException ex:
                    await WriteResponseAsync(
                        context,
                        StatusCodes.Status500InternalServerError,
                        "DatabaseError_500",
                        ex.InnerException?.Message ?? ex.Message);
                    break;

                default:
                    await WriteResponseAsync(
                        context,
                        StatusCodes.Status500InternalServerError,
                        "InternalServerError_500",
                        "Something went wrong. Please contact your administrator.");
                    break;
            }
        }

        private async Task WriteResponseAsync(HttpContext context, int statusCode, string code, string message)
        {
            context.Response.StatusCode = statusCode;

            var response = new GeneralResponseDto(message, code);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}
