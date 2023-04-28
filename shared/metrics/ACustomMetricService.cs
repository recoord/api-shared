using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public abstract class ACustomMetricService : BackgroundService
{
    private readonly string _serviceName;

    public ACustomMetricService(IConfiguration config)
    {
        _serviceName = config["Application:Name"] ?? "service-name-not-present";
    }

    protected string GenerateServiceName(string name)
    {
        return $"{_serviceName}_{name}";
    }
}