
namespace WebLayer.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine($"[Incoming request] : " +
                $"{context.Request.Method} {context.Request.Path}");

            await next(context);

            Console.WriteLine($"[Outgoing response] : " +
                $"{context.Request.Method} {context.Request.Path} -> {context.Response.StatusCode}");
        }
    }
}
