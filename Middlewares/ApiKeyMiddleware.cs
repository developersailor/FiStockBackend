namespace FiStockBackend.Middlewares;

using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-Api-Key"; // You can change this to whatever header name you want.

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Check for the API key in the request header
        if (!httpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync("API key is missing");
            return;
        }

        // Validate the API key (you can check against a stored key or configuration value)
        var validApiKey = "Yu7TEHoDFGwr6ecNoquIsAoMcnmHnGML"; // You should store this securely, e.g., in configuration.
        
        if (extractedApiKey != validApiKey)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync("Invalid API key");
            return;
        }

        // Call the next middleware in the pipeline
        await _next(httpContext);
    }
}