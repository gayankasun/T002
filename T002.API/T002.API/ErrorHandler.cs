using System.Net;

namespace T002.API.T002.API
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ErrorHandler(RequestDelegate delegetor, ILogger<ErrorHandler> logger)
        {
            _next = delegetor;
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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new GenericError()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = $"Message: {ex.Message}",
                    ErrorType = ex.GetType().ToString()
                }.ToString());

                _logger.LogError(ex, "Unhandled error caught on T002.API");
            }
        }
    }
}
