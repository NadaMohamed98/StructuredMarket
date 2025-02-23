using StructuredMarket.Application.Common;
using System.Net;
using System.Text.Json;

namespace StructuredMarket.API.Middlewares
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    var unauthorizedResponse = new ApiResponse<object>(null, "Unauthorized access. Please log in.", false);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(unauthorizedResponse));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    var forbiddenResponse = new ApiResponse<object>(null, "Access denied. You do not have permission.", false);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(forbiddenResponse));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    var notFoundResponse = new ApiResponse<object>(null, "Resource not found.", false);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(notFoundResponse));
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("The AuthorizationPolicy named"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";

                var policyErrorResponse = new ApiResponse<object>(null, "Invalid authorization policy. Please check your configuration.", false);
                await context.Response.WriteAsync(JsonSerializer.Serialize(policyErrorResponse));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new ApiResponse<object>(null, "An unexpected error occurred.", false);
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }

}
