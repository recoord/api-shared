using Microsoft.Extensions.DependencyInjection;

public static class MetricsDependecyInjection
{
    public static void AddMetrics(this IServiceCollection services, bool startHostedService)
    {
        if (startHostedService == false) { return; }

        services.AddHostedService<MetricHostedService>();
    }
}