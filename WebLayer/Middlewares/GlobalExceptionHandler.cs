
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using System;
using System.Security.Authentication;
using System.Text.Json;

namespace WebLayer.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HandleExceptionsAsync(context, ex);
            }
        }

        private void HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ItemNotFoundException ex:
                    context.Response.StatusCode = 404;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case PermissionDeniedException ex:
                    context.Response.StatusCode = 403;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case BaseBussinessException ex:
                    context.Response.StatusCode = 400;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case AuthenticationException ex:
                    context.Response.StatusCode = 401;
                    context.Response.WriteAsync(GenerateResponseBody("AuthenticationError_401", ex.Message));
                    break;
                default:
                    context.Response.StatusCode = 500;
                    context.Response.WriteAsync(GenerateResponseBody(
                        "InternalServerError_500",
                        "Something went wrong. Please contact your administrator."));
                    break;
            }
        }

        private string GenerateResponseBody(string code, string message)
        {
            var response = new GeneralResponseDto(message, code);

            return JsonSerializer.Serialize(response, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
