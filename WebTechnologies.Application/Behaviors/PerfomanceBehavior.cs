using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebTechnologies.Application.Behaviors;
internal class PerfomanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerfomanceBehavior<TRequest, TResponse>> _logger;

    public PerfomanceBehavior(ILogger<PerfomanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        var result = await next();

        sw.Stop();

        _logger.LogInformation("Request: {@RequestName} was executing {@Ms}ms", typeof(TRequest).Name, sw.ElapsedMilliseconds);

        if (sw.ElapsedMilliseconds > TimeSpan.FromSeconds(3).Milliseconds)
        {
            _logger.LogWarning("Request: {@RequestName} was executing too long {@Ms}ms", typeof(TRequest).Name, sw.ElapsedMilliseconds);
        }

        return result;
    }
}
