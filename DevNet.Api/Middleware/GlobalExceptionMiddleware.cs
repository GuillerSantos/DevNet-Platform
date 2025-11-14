using DevNet.Application.Common.Responses;
using System.Net;
using System.Text.Json;

namespace DevNet.Api.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Unhandled Exception Occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static int GetStatusCodeFromException(Exception exception) => exception switch
        {
            ArgumentException or ArgumentNullException => (int) HttpStatusCode.BadRequest,
            KeyNotFoundException => (int) HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int) HttpStatusCode.Unauthorized,
            InvalidOperationException => (int) HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError
        };

        private static string GetErrorMessageFromException(Exception exception, int statusCode)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                return exception.Message;

            return statusCode switch
            {
                (int) HttpStatusCode.BadRequest => "The Request Was Invalid Or Cannot Be Saved.",
                (int) HttpStatusCode.NotFound => "The Requested Resource Was Not Found.",
                (int) HttpStatusCode.Unauthorized => "You Are Not Authorized To Access This Resource.",
                _ => "An Unexpected Error Occurred. Please Try Again Later."
            };
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = GetStatusCodeFromException(exception);

            var errorResponse = new Response
            {
                Success = false,
                ErrorMessage = GetErrorMessageFromException(exception, context.Response.StatusCode)
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var result = JsonSerializer.Serialize(errorResponse, options);
            await context.Response.WriteAsync(result);
        }

        #endregion Private Methods
    }
}