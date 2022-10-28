using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;

public class MetricHostedService : IHostedService
{
    private readonly string Host;
    private readonly int Port;
    private readonly ILogger _logger;

    public MetricHostedService(ILogger<MetricHostedService> logger)
    {
        _logger = logger;
        Host = Environment.GetEnvironmentVariable("METRIC_SERVER_HOST") ?? "0.0.0.0";
        Port = int.Parse(Environment.GetEnvironmentVariable("METRIC_SERVER_PORT") ?? "8080");
    }

    private IMetricServer? _metricServer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting metric server on {Host}:{Port}");
        //_logger.Info($"Starting metric server on {Host}:{Port}");

        _metricServer = new KestrelMetricServer(Host, Port).Start();

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using (_metricServer)
        {
            _logger.LogInformation("Shutting down metric server");
            if (_metricServer != null)
                await _metricServer.StopAsync();
            _logger.LogInformation("Done shutting down metric server");
        }
    }
}