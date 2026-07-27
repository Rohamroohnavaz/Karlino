
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using System;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

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
                Console.WriteLine(ex.ToString());
                await HandleExceptionsAsync(context, ex);
            }
        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ItemNotFoundException ex:
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case PermissionDeniedException ex:
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case BaseBussinessException ex:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case AuthenticationException ex:
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(GenerateResponseBody("AuthenticationError_401", ex.Message));
                    break;
                case DbUpdateException ex:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Database update failed.",
                        detail = ex.InnerException?.Message ?? ex.Message
                    });
                    break;
                //case ArgumentException ex:
                //    context.Response.StatusCode = 400;
                //    await context.Response.WriteAsync(GenerateResponseBody())
                default:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(GenerateResponseBody(
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
