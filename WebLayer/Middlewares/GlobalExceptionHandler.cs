
//using MyFinalProject.Application.ServiceExceptions;
//using System;
//using System.Security.Authentication;

//namespace WebLayer.Middlewares
//{
//    public class GlobalExceptionHandler : IMiddleware
//    {
//        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            try
//            {
//                await next(context);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                HandleExceptionsAsync(context, next);
//            }
//        }

//        private void HandleExceptionsAsync(HttpContext context, RequestDelegate next)
//        {
//            switch (Exception)
//            {
//                case UserNotFoundException ex:
//                    context.Response.StatusCode = 404;
//                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
//                    break;
//                case PermissionDeniedException ex:
//                    context.Response.StatusCode = 403;
//                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
//                    break;
//                case BaseBusinessException ex:
//                    context.Response.StatusCode = 400;
//                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
//                    break;
//                case AuthenticationException ex:
//                    context.Response.StatusCode = 401;
//                    context.Response.WriteAsync(GenerateResponseBody("AuthenticationError_401", ex.Message));
//                    break;
//                default:
//                    context.Response.StatusCode = 500;
//                    context.Response.WriteAsync(GenerateResponseBody(
//                        "InternalServerError_500",
//                        "Something went wrong. Please contact your administrator."));
//                    break;
//            }
//        }
//    }
//}
