namespace WebTechnologies.WebAPI.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation("Executing on path: {@Path}", httpContext.Request.Path);

        await _next(httpContext);

        _logger.LogInformation("Executed on path: {@Path}", httpContext.Request.Path);
    }
}
