using MediatR;
using Microsoft.Extensions.Logging;

namespace WebTechnologies.Application.Behaviors;
internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing request: {@RequestName}", typeof(TRequest).Name);

        var result = await next();

        _logger.LogInformation("Executed request: {@RequestName}", typeof(TRequest).Name);

        return result;
    }
}
