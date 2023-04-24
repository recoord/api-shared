using Microsoft.Extensions.DependencyInjection;
using Prometheus;

public static class MetricsDependecyInjection
{
    public static void AddMetrics(this IServiceCollection services, bool startHostedService)
    {
        if (startHostedService == false) { return; }

        Metrics.DefaultFactory.ExemplarBehavior = ExemplarBehavior.NoExemplars();
        services.AddHostedService<MetricHostedService>();
    }
}