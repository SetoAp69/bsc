using System.Net;

namespace bsc_be.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await ctx.Response.WriteAsJsonAsync(new
                {
                    status = "error",
                    message = "Terjadi kesalahan server."
                });
            }
        }
    }
}